using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType[] enemies;
    public Transform player;
    public float initialSpawnInterval = 2.0f;
    public float spawnIntervalDecrease = 0.1f;
    public float minSpawnInterval = 0.5f;

    private float currentSpawnInterval;
    private float timeSinceLastSpawn;
    private Camera mainCamera;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        mainCamera = Camera.main;
        StartCoroutine(DecreaseSpawnInterval());
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyPrefab = GetRandomEnemyPrefab();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 spawnPosition = Vector3.zero;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), 0, mainCamera.transform.position.z + cameraHeight / 2 + 5);
                break;
            case 1: // Bottom
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2), 0, mainCamera.transform.position.z - cameraHeight / 2 - 5);
                break;
            case 2: // Left
                spawnPosition = new Vector3(mainCamera.transform.position.x - cameraWidth / 2 - 5, 0, Random.Range(-cameraHeight / 2, cameraHeight / 2));
                break;
            case 3: // Right
                spawnPosition = new Vector3(mainCamera.transform.position.x + cameraWidth / 2 + 5, 0, Random.Range(-cameraHeight / 2, cameraHeight / 2));
                break;
        }

        return spawnPosition;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        float totalChance = 0f;
        foreach (EnemyType enemy in enemies)
            totalChance += enemy.spawnChance;

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (EnemyType enemy in enemies)
        {
            cumulativeChance += enemy.spawnChance;
            if (randomValue <= cumulativeChance)
                return enemy.prefab;
        }

        return enemies[0].prefab;
    }

    private IEnumerator DecreaseSpawnInterval()
    {
        while (currentSpawnInterval > minSpawnInterval)
        {
            yield return new WaitForSeconds(10.0f);
            currentSpawnInterval -= spawnIntervalDecrease;
        }
    }
}
