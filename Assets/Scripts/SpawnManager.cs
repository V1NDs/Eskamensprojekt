using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    //public GameObject prefab;
    public GameObject[] models;
    private bool started = false;
    public int wave = 0;
    public int addonEnemiesPerWave = 3;
    public float difficultyMultiplier = 1.0f;
    public float totalEnemies;
    public int totalEnemiesSpawned = 0;
    public int totalEnemiesKilled = 0;
    public GameObject[] SpawnPoints;
    public GameObject medkitSpawner;
    public TMP_Text counterText;
    public TMP_Text waveText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartGame", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;

        if (totalEnemiesSpawned < totalEnemies)
        {
            Vector3 whereToSpawn = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)].transform.position;

            //Spawn one
            Instantiate(models[UnityEngine.Random.Range(0, models.Length)], whereToSpawn, Quaternion.identity);
            medkitSpawner.GetComponent<MedkitSpawner>().SpawnMedkits(whereToSpawn);

            totalEnemiesSpawned += 1;
        }

        if (totalEnemiesSpawned == totalEnemiesKilled)
        {
            wave += 1;
            float newEnemies = addonEnemiesPerWave * difficultyMultiplier;
            totalEnemies = Mathf.Round(totalEnemies + newEnemies);
        }

        counterText.text = totalEnemiesKilled.ToString();
        waveText.text = "Wave " + wave.ToString();

        // Low difficulty faster reload
        if (difficultyMultiplier <= 2.0)
        {
            GameObject.Find("pistol_1").GetComponent<Gun>().reloadFaster = true;
        }
    }

    public void ChangeDifficulty(bool increase)
    {
        if (increase)
        {
            difficultyMultiplier += 0.1f;
        } else
        {
            difficultyMultiplier -= 0.1f;
        }
    }

    // Start the game
    void StartGame()
    {
        wave = 1;
        difficultyMultiplier = 1.0f;
        totalEnemies = addonEnemiesPerWave;
        started = true;
    }
}
