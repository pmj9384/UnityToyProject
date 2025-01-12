using UnityEngine;

public class BoundWood : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 바운드 효과 추가
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null && playerRb.velocity.y <= 0)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 30f, playerRb.velocity.z); // 점프

            }

            // 바닥이 무너지게 만듦
            if (rb != null)
            {
                // rb.isKinematic = false; 
                rb.useGravity = true;   
            }
        }
    }
}
