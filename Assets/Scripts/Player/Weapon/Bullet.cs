using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float maxDistance = float.MaxValue;
    private Vector3 startPosition;

    private void Start() => startPosition = transform.position;
    
    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if (enemy != null)
                enemy.TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}
