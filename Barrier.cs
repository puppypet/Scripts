using UnityEngine;

public class Barrier : MonoBehaviour
{
    [Header("Barrier Settings")]
    public Transform player;        // 플레이어 Transform
    public Transform crossline;     // 회전할 크로스라인 (배리어가 따라갈 기준)
    public float radius = 5f;       // 배리어가 플레이어로부터 떨어진 거리
    public float rotationSpeed = 50f; // 회전 속도
    private float angle;            // 회전 각도

    void Start()
    {
        // 배리어의 초기 위치를 크로스라인에서 설정된 거리만큼 설정
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position = player.position + direction * radius;
    }

    void Update()
    {
        // 마우스 위치를 스크린 좌표로 가져오기
        Vector3 mousePos = Input.mousePosition;

        // 스크린 좌표를 월드 좌표로 변환
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0; // 2D 게임이므로 Z값을 0으로 설정

        // 플레이어 위치와 마우스의 방향 계산
        Vector3 direction = (worldMousePos - player.position).normalized;

        // 회전 각도 계산
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 배리어를 플레이어 주위에서 회전시키기
        transform.position = player.position + new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * angle) * radius, 
            Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 
            0);

        // 배리어 회전 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 객체가 "Bullet" 태그를 가지고 있다면
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // 총알 삭제
        }
    }
}

