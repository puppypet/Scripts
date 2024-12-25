using System.Collections;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;  // 적 프리팹
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform player;        // 플레이어
    public float spawnInterval = 2f; // 적 생성 간격 (초)
    public int maxEnemies = 10;      // 최대 적 수
    private int currentEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies()); // 적 생성 시작
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            // 화면 밖에서 랜덤한 위치에 적 생성
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 적에게 플레이어를 타겟으로 설정하고 총알 프리팹도 설정
            enemy.GetComponent<Enemy2>().SetTarget(player);
            enemy.GetComponent<Enemy2>().bulletPrefab = bulletPrefab; // 총알 프리팹 할당

            // 적의 개수를 증가시킨 후 간격만큼 기다림
            currentEnemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 화면 밖에서 랜덤하게 위치 생성
    Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-960f, 1920f);
        float y = Random.Range(-540f, 1080f);

        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
    }
}
