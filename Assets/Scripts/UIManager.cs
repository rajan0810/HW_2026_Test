using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager: MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gamePanel;

    [Header("Text References")]
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        Time.timeScale = 0f; //Freeze

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void TriggerGameOver(int finalScore)
    {
        Time.timeScale = 0f;

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        finalScoreText.text = "" + finalScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





}