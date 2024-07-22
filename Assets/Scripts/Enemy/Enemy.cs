using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float moveSpeed;
    public int pointsForKill;
    private Transform player;

    void Start() => player = GameObject.FindGameObjectWithTag("Player").transform;
    
    void Update()
    {
        if (player != null)
            MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null && !playerController.IsInvincible())
                playerController.Die();
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        GameManager.Instance.AddPoints(pointsForKill);
        Destroy(gameObject);
    }
}
