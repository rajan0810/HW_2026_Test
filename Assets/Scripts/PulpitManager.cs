using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class PulpitManager: MonoBehaviour
{
    private float lifetime;
    private float spawnTriggerTime;
    private GameManager gameManager;

    private bool hasSpawnedNext = false;
    private float aliveTime = 0f;


    //Animation Variables
    private Vector3 targetScale;
    private float scaleDuration = 0.4f;
    private bool isShrinking = false;

    public void Initialize(float minTime, float maxTime, float spawnTime, GameManager gm)
    {
        lifetime = UnityEngine.Random.Range(minTime, maxTime);
        spawnTriggerTime = spawnTime;
        gameManager = gm;

        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;

        StartCoroutine(ScaleUp());
    }

    void Update()
    {
        aliveTime += Time.deltaTime;

        if (aliveTime >= spawnTriggerTime && !hasSpawnedNext)
        {
            gameManager.SpawnPulpit();
            hasSpawnedNext = true;
        }

        if (aliveTime >= lifetime && !isShrinking)
        {
            isShrinking = true;
            gameManager.RemovePulpitFromList(gameObject);
            StartCoroutine(ScaleDownAndDestroy());
        }
    }

    public void ForceShrinkAndDestroy() // For Game Manager to Call (public)
    {
        if (!isShrinking)
        {
            isShrinking = true;
            StartCoroutine(ScaleDownAndDestroy());
        }
    }

    private IEnumerator ScaleUp()
    {
        float elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime/scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // WAIT FOR NEXT FRAME
        }
        transform.localScale = targetScale; //LOCK EXACTLY TO TARGET SIZE
    }

    private IEnumerator ScaleDownAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;

        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, Vector3.zero, elapsedTime/scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // WAIT FOR NEXT FRAME
        }

        Destroy(gameObject);
    }
}