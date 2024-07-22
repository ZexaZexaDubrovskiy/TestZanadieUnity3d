using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float rotationSpeed = 180.0f;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public Transform bulletSpawn;

    private Vector3 movement;
    private Camera mainCamera;
    private Quaternion targetRotation;
    private bool isShooting = false;

    private float originalMoveSpeed;
    private bool isInvincible = false;

    public string currentWeapon;
    private float weaponDamage;
    private float weaponFireRate;
    private int bulletsPerShot;
    private float fireTimer = 0.0f;

    private bool isDead = false;

    void Start()
    {
        mainCamera = Camera.main;
        originalMoveSpeed = moveSpeed;
        PickUpWeapon("Пистолет", 3, 2, float.MaxValue, 1);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + movement.x, -20.0f, 20.0f),
            transform.position.y,
            Mathf.Clamp(transform.position.z + movement.z, -15.0f, 15.0f)
        );
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
            UpdateTargetRotation();
        }
        else
        {
            isShooting = false;
            fireTimer = 0.0f;
        }

        if (isShooting)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1.0f / weaponFireRate)
            {
                Shoot();
                fireTimer = 0.0f;
            }
        }
    }

    void UpdateTargetRotation()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        if (currentWeapon == "Дробовик")
        {
            float spreadAngle = 10.0f;
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Quaternion bulletRotation = Quaternion.Euler(0, -spreadAngle / 2 + spreadAngle / bulletsPerShot * i, 0);
                InstantiateBullet(bulletRotation);
            }
        }
        else if (currentWeapon == "Гранатомет")
        {
            InstantiateGrenade();
        }
        else
        {
            InstantiateBullet(Quaternion.identity);
        }
    }

    void InstantiateBullet(Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
        bullet.GetComponent<Rigidbody>().velocity = rotation * transform.forward * weaponFireRate;
        bullet.GetComponent<Bullet>().damage = (int)weaponDamage;
        bullet.GetComponent<Bullet>().maxDistance = currentWeapon == "Дробовик" ? 7.0f : float.MaxValue;
        Destroy(bullet, 2.0f);
    }

    void InstantiateGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, bulletSpawn.position, Quaternion.identity);
        grenade.GetComponent<Rigidbody>().velocity = transform.forward * weaponFireRate;
        grenade.GetComponent<Grenade>().damage = (int)weaponDamage;
        grenade.GetComponent<Grenade>().targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
        grenade.GetComponent<Grenade>().targetPosition.y = 0;
    }

    public void PickUpWeapon(string weaponName, float damage, float fireRate, float range, int bulletsPerShot)
    {
        currentWeapon = weaponName;
        weaponDamage = damage;
        weaponFireRate = fireRate;
        this.bulletsPerShot = bulletsPerShot;
    }

    public void StartSpeedBoost(float duration, float multiplier)
    {
        moveSpeed *= multiplier;
        Invoke("EndSpeedBoost", duration);
    }

    public void EndSpeedBoost() => moveSpeed = originalMoveSpeed;
    

    public void StartInvincibility(float duration)
    {
        isInvincible = true;
        Invoke("EndInvincibility", duration);
    }

    void EndInvincibility() => isInvincible = false;
    
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GameManager.Instance.GameOver();
        }
    }

    public bool IsInvincible() => isInvincible;
    
}
