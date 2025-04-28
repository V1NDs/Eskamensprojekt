using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    private bool started = false;
    public int wave = 0;
    public int addonEnemiesPerWave = 3;
    public float difficultyMultiplier = 1.0f;
    public float totalEnemies;
    public int totalEnemiesSpawned = 0;
    public int totalEnemiesKilled = 0;
    public GameObject[] SpawnPoints;
    public GameObject medkitSpawner;

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
            Instantiate(prefab, whereToSpawn, Quaternion.identity);
            medkitSpawner.GetComponent<MedkitSpawner>().SpawnMedkits(whereToSpawn);

            totalEnemiesSpawned += 1;
        }

        if (totalEnemiesSpawned == totalEnemiesKilled)
        {
            wave += 1;
            float newEnemies = addonEnemiesPerWave * difficultyMultiplier;
            totalEnemies = Mathf.Round(totalEnemies + newEnemies);
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
