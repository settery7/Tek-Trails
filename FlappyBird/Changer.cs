using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Changer : MonoBehaviour
{

    public int playerScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public int highScore = 0;

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        if (highScore < playerScore)
        {
            highScore = playerScore;
        }
        currentScoreText.text = playerScore.ToString();
        highScoreText.text = highScore.ToString();
    }
}
