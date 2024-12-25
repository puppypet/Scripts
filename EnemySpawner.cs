using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // 적 프리팹
    public int maxEnemies = 10;    // 최대 생성될 적의 수
    public float spawnInterval = 2f;  // 생성 간격

    private Camera mainCamera;
    private List<GameObject> spawnedEnemies;  // 스폰된 적 객체 리스트

    void Start()
    {
        mainCamera = Camera.main;  // 메인 카메라 참조
        spawnedEnemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);  // 주기적으로 적 생성
    }

    void SpawnEnemy()
    {
        // 최대 적 수를 넘지 않도록 제어
        if (spawnedEnemies.Count >= maxEnemies) return;

        // 화면 외부에서 1만큼 떨어진 위치 계산
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // 적 생성
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 생성된 적이 null이 아닌 경우에만 리스트에 추가
        if (enemy != null)
        {
            spawnedEnemies.Add(enemy);

            // 스폰된 적만 이동하고 총알을 발사하도록 활성화
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.EnableMovementAndShooting();  // 이동 및 총알 발사 활성화
            }
        }
    }

    void Update()
    {
        // 파괴된 적들을 리스트에서 제거
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);  // 파괴된 적 제거
            }
        }
    }

    // 화면 외부에서 1만큼 떨어진 위치 계산
    Vector3 GetRandomSpawnPosition()
    {
        float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        float cameraHeight = mainCamera.orthographicSize * 2;

        // 화면 외부에서 1만큼 떨어진 x, y 범위에서 랜덤 위치 생성
        Vector3 spawnPosition = Vector3.zero;

        // 랜덤하게 좌측, 우측, 상단, 하단 선택하여 1만큼 떨어진 위치 생성
        int side = Random.Range(0, 4);  // 0 = Left, 1 = Right, 2 = Top, 3 = Bottom

        switch (side)
        {
            case 0: // 왼쪽 화면 외부
                spawnPosition = new Vector3(-cameraWidth / 2 - 1, Random.Range(-cameraHeight / 2, cameraHeight / 2), 0);
                break;
            case 1: // 오른쪽 화면 외부
                spawnPosition = new Vector3(cameraWidth / 2 + 1, Random.Range(-cameraHeight / 2, cameraHeight / 2), 0);
                break;
            case 2: // 위쪽 화면 외부
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), cameraHeight / 2 + 1, 0);
                break;
            case 3: // 아래쪽 화면 외부
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), -cameraHeight / 2 - 1, 0);
                break;
        }

        return spawnPosition;
    }
}























