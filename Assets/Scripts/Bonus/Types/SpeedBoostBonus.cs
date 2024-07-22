public class SpeedBoostBonus : Bonus
{
    public float speedMultiplier = 1.5f;
    public float duration = 10.0f;

    public override void ApplyBonus(PlayerController player) => 
        player.StartSpeedBoost(duration, speedMultiplier);
}
