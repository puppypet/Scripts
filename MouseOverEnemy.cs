using UnityEngine;

public class MouseOverEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isInRange = false;  // 크로스헤어가 적과 닿았는지 체크하는 변수

    [Header("Crosshair")]
    public Transform crosshair; // 크로스헤어 Transform

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;  // 원래 색상 저장
        }
    }

    void Update()
    {
        // 크로스헤어와 적의 충돌 확인
        Collider2D col = GetComponent<Collider2D>();
        if (col != null && col.bounds.Contains(crosshair.position))
        {
            // 공격 모드일 때만 색상 변경
            if (PlayerModeManager.Instance.IsAttackMode())
            {
                if (!isInRange)
                {
                    spriteRenderer.color = new Color(0f, 0.988f, 1f);  // #00fcff 색상
                    isInRange = true;
                }
            }
        }
        else
        {
            if (isInRange)
            {
                spriteRenderer.color = originalColor;  // 원래 색상으로 복원
                isInRange = false;
            }
        }

        // 마우스 클릭 시 적이 파괴되는 로직
        if (Input.GetMouseButtonDown(0) && isInRange)
        {
            if (PlayerModeManager.Instance.IsAttackMode())
            {
                // 공격 모드에서만 적 파괴
                DestroyWithFragments();
            }
            else
            {
                Debug.Log("방어 모드에서는 적을 파괴할 수 없습니다.");
            }
        }
    }

    // 적을 파괴하는 메소드
    void DestroyWithFragments()
    {
        // 적의 DestroyEnemy 클래스에서 파괴 로직을 처리하도록 호출
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            DestroyEnemy destroyEnemyScript = enemy.GetComponent<DestroyEnemy>();
            if (destroyEnemyScript != null)
            {
                destroyEnemyScript.DestroyWithFragments();  // DestroyEnemy의 파괴 메서드 호출
            }
        }
    }

    // 외부에서 isInRange를 확인할 수 있게 getter 제공
    public bool IsInRange()
    {
        return isInRange;
    }
}









