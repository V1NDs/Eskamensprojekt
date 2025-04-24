using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    private bool started = false;
    public int wave = 0;
    private int difficulty = 0;
    public int addonEnemiesPerWave = 5;
    public float difficultyMultiplier = 5.0f;
    public float totalEnemies;
    public int totalEnemiesSpawned = 0;
    public int totalEnemiesKilled = 0;
    public GameObject[] SpawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartGame", 5);
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

            totalEnemiesSpawned += 1;
        }

        if (totalEnemiesSpawned == totalEnemiesKilled)
        {
            wave += 1;
            float newEnemies = addonEnemiesPerWave * difficultyMultiplier;
            totalEnemies = Mathf.Round(totalEnemies + newEnemies);
        }
    }

    // Start the game
    void StartGame()
    {
        wave = 1;
        difficulty = 1;
        totalEnemies = addonEnemiesPerWave;
        started = true;
    }
}
