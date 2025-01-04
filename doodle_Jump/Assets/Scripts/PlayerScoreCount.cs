using UnityEngine;

public class PlayerScoreCount : MonoBehaviour
{
    public Transform playerTransform;
    private float lastYPosition;
    public int scorePerUnit = 10;  // Y축 높이당 추가될 점수

    private void Start()
    {
        lastYPosition = transform.position.y; // 시작 높이 저장
    }

    private void Update()
    {
        float currentY = transform.position.y;

        // 플레이어가 Y축으로 올라갔을 경우 점수 추가
        if (currentY > lastYPosition)
        {
            float heightGained = currentY - lastYPosition;
            int scoreToAdd = Mathf.FloorToInt(heightGained * scorePerUnit);  // 높이에 비례한 점수 계산
            TextManager.Instance.AddScore(scoreToAdd);

            lastYPosition = currentY;  // 현재 위치를 마지막 위치로 갱신
        }
    }
}
