using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [Header("Fragment Settings")]
    public GameObject fragmentPrefab;  
    public int fragmentCount = 5;
    public float fragmentForce = 5f;
    public float fragmentRotationSpeed = 360f;
    public float fragmentLifetime = 2f;

    [Header("Camera Shake")]
    private CameraShake cameraShake;

    void Start()
    {
        // 카메라 흔들림 스크립트를 찾음
        cameraShake = FindFirstObjectByType<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogWarning("CameraShake not found in the scene.");
        }
    }

    void Update()
    {
        // 마우스 클릭 시 적이 파괴되도록 처리 (방어 모드 체크 추가)
        if (Input.GetMouseButtonDown(0) && PlayerModeManager.Instance.IsAttackMode())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Collider2D col = GetComponent<Collider2D>();
            if (col != null && col.OverlapPoint(mousePos))
            {
                DestroyWithFragments(); // 파괴 시 파편 생성
            }
        }
    }

    // DestroyWithFragments 메서드를 public으로 변경하여 외부에서 호출할 수 있도록 함
    public void DestroyWithFragments()
    {
        // 방어 모드일 때는 파괴 로직 실행 안 함
        if (!PlayerModeManager.Instance.IsAttackMode())
        {
            Debug.Log("방어 모드에서는 적을 파괴할 수 없습니다.");
            return;
        }

        // 파편 생성
        for (int i = 0; i < fragmentCount; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * fragmentForce, ForceMode2D.Impulse);
            }

            Fragment fragmentScript = fragment.GetComponent<Fragment>();
            if (fragmentScript != null)
            {
                fragmentScript.rotationSpeed = Random.Range(-fragmentRotationSpeed, fragmentRotationSpeed);
            }

            Destroy(fragment, fragmentLifetime); // 파편이 일정 시간 후 사라짐
        }

        // 카메라 흔들림 발생
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera();
        }

        // 점수 증가
        ScoreText.AddScore(1);  // 적 파괴 시 점수 증가

        // 적 삭제
        Destroy(gameObject);  // 적 삭제
    }
}







