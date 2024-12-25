using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;       // 총알 속도
    private Transform target;       // 플레이어를 목표로 삼기 위한 변수

    void Update()
    {
        // 목표인 플레이어가 있을 때, 플레이어를 향해 이동
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    // 플레이어를 목표로 설정하는 메소드
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // 플레이어와 충돌 시 비활성화
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌했을 때
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the Player!");
            // 총알 비활성화 (삭제하지 않고 비활성화)
            gameObject.SetActive(false);
        }
    }
}





