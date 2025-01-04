using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene Management 재시작용

public class PlayerHealth : LivingEntity
{
    public AudioClip deathClip;
    public AudioClip hitSound;
    //public AudioClip itemPickupSound;
    private AudioSource audioSource;
    // private Animator animator;
    private PlayerMovement movement;
    private PlayerShooter shooter;
    private bool isPaused = false;
    public float fallLimit = -10f;  // Y축 아래로 떨어질 시 사망
    private float startYPosition; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerShooter>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        // healthSlider.gameObject.SetActive(true);
 
        // healthSlider.maxValue = maxHp;
        // healthSlider.minValue = 0f;
        // healthSlider.value = Hp;

        startYPosition = transform.position.y;
        movement.enabled = true;
        shooter.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        // healthSlider.value = Hp;
        if (!IsDead)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
    protected override void Die()
    {
        base.Die();

        // healthSlider.gameObject.SetActive(false);
        // animator.SetTrigger("Die");

        audioSource.PlayOneShot(deathClip);
        movement.enabled = false;
        shooter.enabled = false;

        TextManager.Instance.ShowGameOver();

        Time.timeScale = 0f; 
    }
    public override void AddHp(float add)
    {
        base.AddHp(add);
        // healthSlider.value = Hp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        if (transform.position.y < startYPosition + fallLimit && !IsDead)
        {
            Die();  // Y축 아래로 떨어졌을 때 사망 처리
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (IsDead)
        //  return;
        // if (other.CompareTag("Item"))
        // {
        //     var item = other.GetComponent<IItem>();
        //     item?.Use(gameObject);

        //     audioSource.PlayOneShot(itemPickupSound);
          
        // }

        if (!IsDead && other.CompareTag("Enemy"))
        {
            Die();  // 적과 충돌하면 사망
        }
    }

    public void TogglePause()
    {   
        if (isPaused)
        {
        Time.timeScale = 1f;  // 게임 재개
        TextManager.Instance.HidePause();  // Pause 텍스트 비활성화
        isPaused = false;
        }
        else
        {
        Time.timeScale = 0f;  // 게임 멈춤
        TextManager.Instance.ShowPause();  // Pause 텍스트 활성화
        isPaused = true;
        }
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;  // 게임 재개
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 현재 씬 다시 로드
    }

}

