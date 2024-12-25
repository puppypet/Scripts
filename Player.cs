using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리용

public class Player : MonoBehaviour
{
    public float health = 1f;  // 플레이어의 체력 (예시)
    private ScoreManager scoreManager;  // ScoreManager 참조

    void Start()
    {
        // 씬에서 ScoreManager 객체 찾기
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager가 씬에 없습니다.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Bullet"))
        {
            // 적 또는 총알과 충돌 시 체력 감소
            health -= 1;
            
            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        // 게임오버 로직
        Debug.Log("Game Over!");
        Time.timeScale = 0f;  // 게임 시간 멈추기

        // 점수 초기화
        if (scoreManager != null)
        {
            scoreManager.ResetScore();  // 게임 오버 시 점수 초기화
        }
    }

    // R 키로 재시작
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 게임을 재시작하는 로직
            Time.timeScale = 1f;  // 게임 시간 재개

            // 씬 리로드 (게임을 초기화)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // 점수 초기화
            if (scoreManager != null)
            {
                scoreManager.ResetScore();  // 점수 초기화
            }
        }
    }
}





