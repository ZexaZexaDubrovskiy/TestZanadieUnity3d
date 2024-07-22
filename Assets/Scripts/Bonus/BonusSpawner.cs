using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
    public WeaponData[] weapons;
    public GameObject speedBoostPrefab;
    public GameObject invincibilityPrefab;
    public Transform player;
    public float spawnRadius = 5.0f;

    private Camera mainCamera;
    private PlayerController playerController;

    private void Start()
    {
        mainCamera = Camera.main;
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(SpawnWeaponBonus());
        StartCoroutine(SpawnTemporaryBonus());
    }

    private Vector3 GetRandomPositionInView()
    {
        Vector3 randomViewportPoint = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), mainCamera.nearClipPlane);
        Vector3 randomWorldPoint = mainCamera.ViewportToWorldPoint(randomViewportPoint);
        randomWorldPoint.y = 0;
        return randomWorldPoint;
    }

    private IEnumerator SpawnWeaponBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            List<WeaponData> availableWeapons = new List<WeaponData>();
            foreach (var weapon in weapons)
            {
                if (weapon.name != playerController.currentWeapon)
                    availableWeapons.Add(weapon);
            }
            if (availableWeapons.Count > 0)
            {
                WeaponData weaponToSpawn = availableWeapons[Random.Range(0, availableWeapons.Count)];
                GameObject bonus = Instantiate(weaponToSpawn.prefab, GetRandomPositionInView(), Quaternion.identity);
                Destroy(bonus, 5.0f);
            }
        }
    }

    IEnumerator SpawnTemporaryBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(27.0f);
            GameObject bonusToSpawn = Random.Range(0, 2) == 0 ? speedBoostPrefab : invincibilityPrefab;
            GameObject bonus = Instantiate(bonusToSpawn, GetRandomPositionInView(), Quaternion.identity);
            Destroy(bonus, 5.0f);
        }
    }
}
