using UnityEngine;

public class RhythmObject : MonoBehaviour
{
    [Header("Rhythm Settings")]
    public float beatDuration = 1f;      // 비트 주기 (리듬 타는 속도) : 한 박자의 길이
    public float scaleAmplitude = 0.5f;  // 크기 변화의 최대 크기 변화량
    public float minScale = 0.5f;        // 최소 크기
    public float maxScale = 2f;          // 최대 크기

    private float time;                  // 시간 변수
    private Vector3 originalScale;       // 원래 크기

    void Start()
    {
        originalScale = transform.localScale;  // 원래 크기 저장
    }

    void Update()
    {
        time += Time.deltaTime;  // 시간 누적

        // 비트 주기를 따라 크기 변화
        float scaleFactor = Mathf.Sin(time * Mathf.PI * 2 / beatDuration);

        // "따"일 때 커지고, "ㅡㅡㅡ"일 때 작아짐 (scaleFactor로 변화를)
        // scaleFactor는 -1에서 1 사이로 반복하므로, 이 값을 이용해 크기를 변화시킴
        float adjustedScale = Mathf.Abs(scaleFactor); // 절대값을 이용하여 부드럽게

        // 크기 변화 적용
        transform.localScale = originalScale * Mathf.Lerp(minScale, maxScale, adjustedScale);
    }
}
