using UnityEngine;
using TMPro;  //

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // 싱글톤 인스턴스
    public TextMeshProUGUI scoreText;     // 점수 UI 텍스트

    private int score = 0;                // 점수

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 중복된 인스턴스 제거
        }

        DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 객체 유지
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();  // 점수 텍스트 업데이트
        }
    }
}







