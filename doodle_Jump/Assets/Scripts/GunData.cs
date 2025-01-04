using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GunData", fileName = "GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25f;         // 총알 데미지
    public float fireDistance = 50f;   // 사정 거리
    public float fireRate = 0.12f;     // 연사 속도
    public float reloadTime = 1f;      // 리로드 시간

    public int startAmmoRemain = 100;  // 전체 총알 개수
    public int magCapacity = 100;       // 탄창 용량

    public Vector3 firePositionOffset = Vector3.zero;  // 발사 위치 오프셋
}