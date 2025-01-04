using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCount : MonoBehaviour
{
    public TMP_Text scoreText;    
    public static int score;

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // UI에 점수 표시
        }
    }
}
