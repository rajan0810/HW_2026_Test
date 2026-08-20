using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager: MonoBehaviour
{
    [Header("References")]
    public TextAsset jsonFile;
    public PlayerController playerController;

    [Header("Parsed Data (Visible for Debugging)")]
    public GameData gameData;

    [Header("Pulpit Spawning")]
    public GameObject pulpitPrefab;
    private Vector3 lastPulpitPosition = Vector3.zero;


    void Start()
    {
        LoadGameData();
    }

    void LoadGameData()
    {
        if (jsonFile != null)
        {
            gameData = JsonUtility.FromJson<GameData>(jsonFile.text);

            if (playerController != null)
            {
                playerController.speed = gameData.player_data.speed;
                Debug.Log("Player speed set to: " + playerController.speed);
            }

            SpawnFirstPulpit();
        }
        else
        {
            Debug.LogError("JSON File not assigned in Game Manager");
        }
    }

    void SpawnFirstPulpit()
    {
        GameObject firstPulpit = Instantiate(pulpitPrefab, Vector3.zero, Quaternion.identity);
        PulpitManager pulpitManager = firstPulpit.GetComponent<PulpitManager>();

        pulpitManager.Initialize(
            gameData.pulpit_data.min_pulpit_destroy_time, 
            gameData.pulpit_data.max_pulpit_destroy_time,
            gameData.pulpit_data.pulpit_spawn_time,
            this
        );

        lastPulpitPosition = Vector3.zero;
    }


    public void SpawnPulpit()
    {
        Vector3 spawnPosition = RandomAdjacent(lastPulpitPosition);

        GameObject newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);

        PulpitManager pulpitManager = newPulpit.GetComponent<PulpitManager>();
        pulpitManager.Initialize(gameData.pulpit_data.min_pulpit_destroy_time, gameData.pulpit_data.min_pulpit_destroy_time, gameData.pulpit_data.pulpit_spawn_time, this);

        lastPulpitPosition = spawnPosition;
    }

    Vector3 RandomAdjacent(Vector3 currentPos)
    {
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward * 9,
            Vector3.back * 9,
            Vector3.left * 9,
            Vector3.right * 9
        };

        int randomIndex = UnityEngine.Random.Range(0, directions.Length);
        return currentPos + directions[randomIndex];
    }


}