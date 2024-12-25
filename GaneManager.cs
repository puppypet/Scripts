using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGameOver = false; // 게임오버 상태 확인

    void Awake()
    {
        // Singleton 패턴을 사용하여 게임 매니저 인스턴스를 전역에서 접근 가능하게 함
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 경우 중복된 인스턴스 제거
        }
    }

    void Update()
    {
        // 게임오버 상태에서 R 키를 누르면 게임 재시작
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // 게임오버 상태를 설정하는 메서드
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("게임 오버!");
            // 추가적인 게임오버 처리 (UI 표시 등)
        }
    }

    // 게임을 재시작하는 메서드
    public void RestartGame()
    {
        // 현재 씬을 다시 로드하여 게임을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


