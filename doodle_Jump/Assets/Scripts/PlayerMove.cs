using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;             // 이동 속도
    public float rotationSpeed = 10f;    // 회전 속도

    public Transform cameraTransform;    // 카메라 Transform

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 잠금
        Cursor.visible = false;                    // 마우스 숨김
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");  // A, D 입력
        float vertical = Input.GetAxis("Vertical");      // W, S 입력

        // **이동 방향 계산 (카메라 기준)**
        Vector3 moveInput = (cameraTransform.right * horizontal + cameraTransform.forward * vertical).normalized;
        moveInput.y = 0f;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            // **이동 처리**
            Vector3 moveDirection = moveInput * speed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);

            // **회전 처리 (이동 방향을 바라보게)**
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
