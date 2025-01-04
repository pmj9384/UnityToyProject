using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public enum MoveAxis { X, Z }  // X축 또는 Z축 이동 선택
    public MoveAxis moveAxis = MoveAxis.X;  // 기본 이동 축을 X축으로 설정
    public float moveDistance;  // 이동할 최대 거리
    public float moveSpeed;  // 이동 속도

    private Vector3 startPosition;  // 시작 위치 저장
    private int direction = 1;  // 이동 방향 (1: 정방향, -1: 반대 방향)

    private void Start()
    {
        startPosition = transform.position;  // 블록의 시작 위치 저장
    }

    private void Update()
    {
        // 이동할 방향 벡터
        Vector3 moveVector = moveAxis == MoveAxis.X ? Vector3.right : Vector3.forward;
        transform.position += moveVector * direction * moveSpeed * Time.deltaTime;

        // 이동 거리 확인
        float currentDistance = Vector3.Distance(startPosition, transform.position);
        if (currentDistance >= moveDistance)
        {
            direction *= -1;  // 이동 방향 반전
        }
    }
}
