using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class PulpitManager: MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool hasBeenScored = false;
    private float lifetime;
    private float spawnTriggerTime;
    private GameManager gameManager;

    private bool hasSpawnedNext = false;
    private float aliveTime = 0f;


    //Animation Variables
    private Vector3 targetScale;
    private float scaleDuration = 0.4f;
    private bool isShrinking = false;

    private MeshRenderer pulpitRenderer;
    private Color originalColor;
    private Color warningColor;

    public void Initialize(float minTime, float maxTime, float spawnTime, GameManager gm)
    {
        lifetime = UnityEngine.Random.Range(minTime, maxTime);
        spawnTriggerTime = spawnTime;
        gameManager = gm;

        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;

        pulpitRenderer = GetComponent<MeshRenderer>();
        originalColor = pulpitRenderer.material.color;

        warningColor = originalColor * 0.2f;

        timerText.text = lifetime.ToString("F2");

        StartCoroutine(ScaleUp());
    }

    void Update()
    {
        aliveTime += Time.deltaTime;
        timerText.text = (lifetime - aliveTime).ToString("F2");

        if (aliveTime >= spawnTriggerTime && !hasSpawnedNext)
        {
            gameManager.SpawnPulpit();
            hasSpawnedNext = true;

            StartCoroutine(BlinkWarning());
        }

        if (aliveTime >= lifetime && !isShrinking)
        {
            isShrinking = true;
            gameManager.RemovePulpitFromList(gameObject);
            StartCoroutine(ScaleDownAndDestroy());
        }
    }

    private IEnumerator BlinkWarning()
    {
        float blinkSpeed = 4f;

        while (true)
        {
            float lerpValue = Mathf.PingPong(Time.time * blinkSpeed, 1f);

            pulpitRenderer.material.color = Color.Lerp(originalColor, warningColor, lerpValue);

            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenScored && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLIDED!");

            hasBeenScored = true; // LOCK
            gameManager.IncreaseScore();
        }
    }

    public void SetAsFirstPulpit()
    {
        hasBeenScored = true;
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