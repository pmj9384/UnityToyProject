using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    public static TextManager Instance; 
    public TMP_Text scoreText;    // 스코어 텍스트
    public TMP_Text gameOverText; // 게임오버 텍스트

    public TMP_Text pauseText; // 일시정지 텍스트
   // public TMP_Text levelText;    // 레벨 표시 텍스트 (예시)
    private int score;


private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else
    {
        Destroy(gameObject); // 중복된 인스턴스가 생성되지 않도록
    }
}

    private void Start()
    {
        score = 0;
        UpdateScoreUI();
        gameOverText.gameObject.SetActive(false); // 처음에 Game Over 텍스트 비활성화
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }


    public void ShowPause()
    {
        pauseText.gameObject.SetActive(true);
    }

    public void HidePause()
    {
        pauseText.gameObject.SetActive(false);
    }




    // public void UpdateLevelUI(int level)
    // {
    //     if (levelText != null)
    //     {
    //         levelText.text = "Level " + level;
    //     }
    // }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
