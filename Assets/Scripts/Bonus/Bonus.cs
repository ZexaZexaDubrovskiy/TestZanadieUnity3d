using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    public abstract void ApplyBonus(PlayerController player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyBonus(player);
                Destroy(gameObject);
            }
        }
    }
}
