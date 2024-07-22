public class WeaponBonus : Bonus
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;
    public int bulletsPerShot;

    public override void ApplyBonus(PlayerController player) =>
        player.PickUpWeapon(weaponName, damage, fireRate, range, bulletsPerShot);
}
