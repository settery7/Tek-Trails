using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodDropManager : MonoBehaviour
{

    public int playerScore;
    public int misses;
    public int maxmisses;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public int highScore = 0;

    [ContextMenu("Increase Score")]

    private void Start()
    {
        playerScore = 0;
        misses = 0;
    }

    private void Update()
    {
        if (misses >= 8)
        {
            gameOver();
        }
    }
    public void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

    public void addmiss()
    {
        misses++;
        missText.text = misses.ToString();
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
