using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : LivingEntity
{
    public ParticleSystem hitEffect;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal); // 기본 데미지 처리

        // 히트 이펙트 및 사운드 효과
        if (hitEffect != null)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();
        }

        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    protected override void Die()
    {
        base.Die(); // 기본 사망 처리
        Destroy(gameObject, 0.1f); 
    }
}

