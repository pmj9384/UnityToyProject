using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;     // 총알 속도
    public float damage = 25f;    // 총알 데미지
    public float lifeTime = 3f;   // 총알 생명 시간

    private IObjectPool<GameObject> pool;  // Object Pool 참조

    public void Launch(Vector3 direction, IObjectPool<GameObject> objectPool)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }

        pool = objectPool; // 발사 시 풀을 참조
        Invoke(nameof(ReturnToPool), lifeTime); // 일정 시간 후 풀로 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDamage(damage, transform.position, -transform.forward);
            }

            ReturnToPool();
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(gameObject); // Object Pool에 반환
        }
        else
        {
            Destroy(gameObject); // 예외적으로 풀이 없으면 Destroy
        }
    }
}
