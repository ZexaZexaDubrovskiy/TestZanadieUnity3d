using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class DangerZoneSpawner : MonoBehaviour
{
    public GameObject slowZonePrefab;
    public GameObject deathZonePrefab;
    public int slowZoneCount = 3;
    public int deathZoneCount = 2;
    public float minDistanceBetweenZones = 3.0f;
    public Vector2 mapBoundsX = new Vector2(-20.0f, 20.0f);
    public Vector2 mapBoundsZ = new Vector2(-15.0f, 15.0f);

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        SpawnZones(slowZonePrefab, slowZoneCount);
        SpawnZones(deathZonePrefab, deathZoneCount);
    }

    void SpawnZones(GameObject zonePrefab, int count)
    {
        int attempts = 0;
        int spawnedCount = 0;
        float radius = zonePrefab.transform.localScale.x;
        while (spawnedCount < count && attempts < 100)
        {
            Vector3 potentialPosition = GetRandomPosition(radius);

            if (IsPositionValid(potentialPosition, radius))
            {
                Instantiate(zonePrefab, potentialPosition, Quaternion.identity);
                spawnPositions.Add(potentialPosition);
                spawnedCount++;
            }

            attempts++;
        }
    }

    Vector3 GetRandomPosition(float radius)
    {
        float x = Random.Range(mapBoundsX.x + radius + minDistanceBetweenZones, mapBoundsX.y - radius - minDistanceBetweenZones);
        float z = Random.Range(mapBoundsZ.x + radius + minDistanceBetweenZones, mapBoundsZ.y - radius - minDistanceBetweenZones);
        return new Vector3(x, 0, z);
    }

    bool IsPositionValid(Vector3 position, float radius)
    {
        foreach (var spawnPosition in spawnPositions)
        {
            if (Vector3.Distance(position, spawnPosition) < radius * 2 + minDistanceBetweenZones)
                return false;
        }
        return true;
    }
}
