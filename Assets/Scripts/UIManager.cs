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

    [Header("Game Manager Reference")]
    public GameManager gameManager;

    void Start()
    {
        Time.timeScale = 0f; //Freeze

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        AudioManager.instance.PlayButtonClick();
        
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
        AudioManager.instance.PlayButtonClick();
        AudioManager.instance.PlayBGM();

        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);

        gameManager.ResetGame();

        Time.timeScale = 1f;

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        AudioManager.instance.PlayButtonClick();

        AudioManager.instance.PlayBGM();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}