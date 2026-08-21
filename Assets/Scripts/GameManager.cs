using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

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
    private List<GameObject> activePulpits = new List<GameObject>();

    [Header("UI and Scoring")]
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public UIManager uiManager;

    [Header("Game State")]
    private bool isGameOver = false;

    void Start()
    {
        LoadGameData();

        UpdateScoreUI();
    }

    void Update()
    {
        if (!isGameOver && playerController != null && playerController.transform.position.y < -2f)
        {
            isGameOver = true;
            uiManager.TriggerGameOver(score);
        }
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

        pulpitManager.SetAsFirstPulpit();

        lastPulpitPosition = Vector3.zero;
        activePulpits.Add(firstPulpit);
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "" + score;
    }


    public void SpawnPulpit()
    {
        if (activePulpits.Count >= 2)
        {
            if (activePulpits[0] != null)
            {
                activePulpits[0].GetComponent<PulpitManager>().ForceShrinkAndDestroy();
            }
            activePulpits.RemoveAt(0);
        }

        Vector3 spawnPosition = RandomAdjacent(lastPulpitPosition);

        GameObject newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);

        PulpitManager pulpitManager = newPulpit.GetComponent<PulpitManager>();

        pulpitManager.Initialize(gameData.pulpit_data.min_pulpit_destroy_time, gameData.pulpit_data.min_pulpit_destroy_time, gameData.pulpit_data.pulpit_spawn_time, this);

        lastPulpitPosition = spawnPosition;
        activePulpits.Add(newPulpit);
    }

    public void RemovePulpitFromList(GameObject pulpit)
    {
        if (activePulpits.Contains(pulpit))
        {
            activePulpits.Remove(pulpit);
        }
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

    public void ResetGame()
    {
        foreach (GameObject pulpit in activePulpits)
        {
            if (pulpit != null) Destroy(pulpit);
        }
        activePulpits.Clear();

        score = 0;
        UpdateScoreUI();

        playerController.transform.position = new Vector3(0f, 1f, 0f);

        Rigidbody rb = playerController.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        SpawnFirstPulpit();
        isGameOver = false;
    }


}