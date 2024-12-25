using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Transform player;        // 플레이어의 Transform
    public Transform crossline;     // 회전할 크로스라인
    public Transform crosshair;     // 크로스헤어 (마우스를 그대로 따라가게)
    public float crosslineLength = 5f;  // 크로스라인 길이 (최대)

    void Update()
    {
        // 1. 마우스 위치를 스크린 좌표로 가져오기
        Vector3 mousePos = Input.mousePosition;

        // 2. 스크린 좌표를 월드 좌표로 변환
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0; // 2D 게임이므로 Z값을 0으로 설정

        // 3. 플레이어 위치와 마우스의 방향 계산
        Vector3 direction = (worldMousePos - player.position).normalized;

        // 4. 크로스라인을 마우스 방향으로 회전
        // Vector3.forward를 기준으로 Z축 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. 각도를 반전시켜서 마우스를 향하도록 수정
        angle -= 180f;

        // 6. 크로스라인의 회전값을 계산
        crossline.rotation = Quaternion.Euler(0, 0, angle);

        // 7. 크로스헤어의 위치 계산 (크로스헤어는 마우스를 그대로 따라가게)
        Vector3 crosshairPos = worldMousePos;

        // 크로스헤어 위치 적용
        crosshair.position = crosshairPos;
    }
}



















