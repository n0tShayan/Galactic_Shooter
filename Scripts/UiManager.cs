using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI scoreUI;
    public GameObject gameOverScreen;
    int currentScore = 0;

    private void Start()
    {

        if(healthUI != null && scoreUI != null && gameOverScreen != null)
        {
            healthUI.text = "100";
            scoreUI.text = "0";
            gameOverScreen.SetActive(false);
        }
       

    }


    public void quitGame()
    {
        Application.Quit();
    }
    public void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void restartGame()
    {
        print("Restarting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void showGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
    public void updateHealthUI(float health)
    {
        healthUI.text = health.ToString();
    }

    public void addScore(int score)
    {
        currentScore += score;
        scoreUI.text = currentScore.ToString();
    }
}
