using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;           // 적 이동 속도 (인스펙터에서 조정 가능)
    public GameObject bulletPrefab;        // 총알 프리팹
    public float bulletSpeed = 5f;         // 총알 속도
    public float fireInterval = 2f;        // 총알 발사 주기

    private Transform player;              // 플레이어의 Transform
    private bool canMove = false;          // 이동 가능 여부
    private bool canShoot = false;         // 총알 발사 가능 여부
    private float fireTimer = 0f;          // 총알 발사 타이머

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // 움직이는 적만 플레이어 추적
            if (canMove)
            {
                MoveTowardsPlayer();
            }

            // 총알 발사 가능하면 발사
            if (canShoot)
            {
                fireTimer += Time.deltaTime;
                if (fireTimer >= fireInterval)
                {
                    FireBullet();
                    fireTimer = 0f; // 타이머 초기화
                }
            }
        }
    }

    // 플레이어를 추적하는 메서드
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // 총알 발사
    private void FireBullet()
    {
        if (bulletPrefab != null)
        {
            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // 총알 속도 설정
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * bulletSpeed;
            }

            Debug.Log("Bullet Fired");
        }
    }

    // 이동 및 총알 발사 활성화
    public void EnableMovementAndShooting()
    {
        canMove = true;
        canShoot = true;
    }
}


















