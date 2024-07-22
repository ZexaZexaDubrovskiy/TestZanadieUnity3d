using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float duration = 10.0f;
    public float multiplier = 0.6f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                player.StartSpeedBoost(duration, multiplier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                player.EndSpeedBoost();
        }
    }
}
