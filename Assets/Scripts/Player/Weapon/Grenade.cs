using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;
    public float maxRaduisExplode = 2.0f;
    public Vector3 targetPosition;
    private bool hasExploded = false;

    private void Start() => targetPosition.y = transform.position.y;
    
    private void Update()
    {
        if (!hasExploded && Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            Explode();
    }

    private void Explode()
    {
        hasExploded = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRaduisExplode);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                
                if (enemy != null)
                    enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
