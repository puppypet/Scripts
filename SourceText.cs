using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public static int scoreValue = 0;

    void Start()
    {
        // TextMeshProUGUI 컴포넌트를 가져옵니다.
        text = GetComponent<TextMeshProUGUI>();  

        // text가 null일 경우 오류 메시지를 출력
        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI 컴포넌트가 할당되지 않았습니다.");
        }

        // 텍스트 가운데 정렬 설정
        text.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {
        // 점수만 표시
        text.text = scoreValue.ToString();  
    }

    // 점수 증가 메서드
    public static void AddScore(int amount)
    {
        scoreValue += amount;
    }
}
