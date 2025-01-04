using UnityEngine;

public class PlayerScoreCount : MonoBehaviour
{
    public Transform playerTransform;
    private float lastYPosition;
    public int scorePerUnit = 10;  
    private void Start()
    {
        lastYPosition = transform.position.y; 
    }

    private void Update()
    {
        float currentY = transform.position.y;
        if (currentY > lastYPosition)
        {
            float heightGained = currentY - lastYPosition;
            int scoreToAdd = Mathf.FloorToInt(heightGained * scorePerUnit);  
            TextManager.Instance.AddScore(scoreToAdd);

            lastYPosition = currentY;  
        }
    }
}
