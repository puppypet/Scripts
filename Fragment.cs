using UnityEngine;

public class Fragment : MonoBehaviour
{
    [HideInInspector] public float rotationSpeed; // 회전 속도

    void Update()
    {
        // Z축을 기준으로 회전
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
