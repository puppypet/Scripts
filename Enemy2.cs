using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;          // 플레이어를 타겟으로 설정

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;   // 총알 프리팹
    public float bulletSpeed = 10f;   // 총알 속도 (인스펙터에서 조절 가능)

    [Header("Fire Settings")]
    public float fireInterval = 2f;   // 총알 발사 주기 (인스펙터에서 조절 가능)

    private float fireTimer = 0f;     // 총알 발사 타이머
    private bool canShoot = false;    // 총을 쏠 수 있는지 여부
    private bool hasFiredOnce = false; // 첫 발사 여부 체크

    void Start()
    {
        fireTimer = 0f;
        canShoot = false;
        hasFiredOnce = false;  // 처음엔 발사하지 않음
    }

    void Update()
    {
        // 플레이어가 공격모드일 때만 총을 쏠 수 있게 처리
        if (canShoot && PlayerModeManager.Instance.IsAttackMode()) // 공격모드일 때만 발사
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireInterval)
            {
                FireBullet();
                fireTimer = 0f; // 타이머 초기화
            }
        }
    }

    void OnBecameVisible()
    {
        // 적이 화면에 들어왔을 때 발사 활성화
        if (!canShoot) 
        {
            fireTimer = 0f;
            canShoot = true; // 화면에 들어오면 발사 가능 상태

            // 첫 발사만 실행하도록 조건 추가
            if (!hasFiredOnce)
            {
                FireBullet();  // 첫 발 발사
                hasFiredOnce = true;  // 첫 발사 완료 플래그 설정
            }
        }
    }

    void OnBecameInvisible()
    {
        // 적이 화면 밖으로 나가면 발사를 멈춤
        canShoot = false;
    }

    void FireBullet()
    {
        if (bulletPrefab != null && player != null)
        {
            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Bullet 스크립트를 통해 추적 설정
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(player);
                bulletScript.speed = bulletSpeed;
            }

            Debug.Log("Bullet Fired"); // 디버그 메시지
        }
        else
        {
            Debug.LogWarning("Bullet prefab or Player target is not assigned!");
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}

















