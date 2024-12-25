using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeDuration = 0.5f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.2f; // 흔들림 강도
    public int shakeRepeats = 10; // 흔들림 반복 횟수

    private Vector3 originalPosition; // 카메라 원래 위치
    private PlayerModeManager playerModeManager; // 플레이어 모드 관리 스크립트 참조

    void Start()
    {
        originalPosition = transform.localPosition;
        playerModeManager = PlayerModeManager.Instance; // 싱글턴 인스턴스 참조
    }

    public void ShakeCamera()
    {
        if (playerModeManager != null && playerModeManager.IsAttackMode()) // 공격 모드일 때만 쉐이크
        {
            CancelInvoke(); // 이전 흔들림 중단
            InvokeRepeating("PerformShake", 0f, shakeDuration / shakeRepeats);
            Invoke("StopShake", shakeDuration);
        }
    }

    private void PerformShake()
    {
        float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
        float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);

        transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f);
    }

    private void StopShake()
    {
        CancelInvoke("PerformShake");
        transform.localPosition = originalPosition;
    }
}


