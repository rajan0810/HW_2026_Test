using UnityEngine;
using System;
using System.Collections.Generic;

public class PulpitManager: MonoBehaviour
{
    private float destroyTimer;
    private float spawnTriggerTime;
    private GameManager gameManager;

    private bool hasSpawned = false;

    public void Initialize(float minTime, float maxTime, float spawnTime, GameManager gm)
    {
        destroyTimer = UnityEngine.Random.Range(minTime, maxTime);
        spawnTriggerTime = spawnTime;
        gameManager = gm;
    }

    void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= spawnTriggerTime && !hasSpawned)
        {
            gameManager.SpawnPulpit();
            hasSpawned = true;
        }

        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}