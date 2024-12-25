using UnityEngine;

public class PlayerModeManager : MonoBehaviour
{
    public static PlayerModeManager Instance;  // 싱글턴 인스턴스
    private bool isAttackMode = true;          // 기본적으로 공격 모드로 설정 (게임 시작 시 공격 모드)
    public GameObject crosshair;               // 크로스헤어
    public GameObject barrier;                 // 방어용 배리어
    public GameObject barrier2;                 // 방어용 배리어2

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        // 게임 시작 시 공격 모드로 설정 (기본적으로 활성화)
        if (isAttackMode)
        {
            crosshair.SetActive(true);  // 크로스헤어 활성화
            barrier.SetActive(false);   // 배리어 비활성화
            barrier2.SetActive(false);  // 배리어2 비활성화
        }
    }

    void Update()
    {
        // 우클릭으로 모드 전환
        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            isAttackMode = !isAttackMode;

            // 공격 모드일 경우
            if (isAttackMode)
            {
                crosshair.SetActive(true);  // 크로스헤어 활성화
                barrier.SetActive(false);   // 배리어 비활성화
                barrier2.SetActive(false);  // 배리어2 비활성화
            }
            else // 방어 모드일 경우
            {
                crosshair.SetActive(false); // 크로스헤어 비활성화
                barrier.SetActive(true);    // 배리어 활성화
                barrier2.SetActive(true);  // 배리어2 활성화
            }
        }
    }

    // 현재 공격 모드인지 확인하는 메서드
    public bool IsAttackMode()
    {
        return isAttackMode;
    }
}






