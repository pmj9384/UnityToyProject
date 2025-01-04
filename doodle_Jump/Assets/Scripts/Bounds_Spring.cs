using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds_Spring : MonoBehaviour
{
    public float bounceForce; // 바운스 힘

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null && playerRb.velocity.y <= 0)
            {
                // 점프 높이를 bounceForce로 직접 설정
                playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);

                Debug.Log("Bounce!");
            }
        }
    }


    private void PlayBounceSound()
    {
        // 여기서 사운드 재생 로직 추가
        Debug.Log("Play bounce sound!");
    }
}
