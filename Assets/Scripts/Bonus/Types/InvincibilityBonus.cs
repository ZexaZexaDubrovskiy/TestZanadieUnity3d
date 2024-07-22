public class InvincibilityBonus : Bonus
{
    public float duration = 10.0f;

    public override void ApplyBonus(PlayerController player) =>
        player.StartInvincibility(duration);
}
