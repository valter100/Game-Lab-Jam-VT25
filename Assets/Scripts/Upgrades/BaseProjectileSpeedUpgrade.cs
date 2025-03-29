using UnityEngine;

public class BaseProjectileSpeedUpgrade : BaseUpgrade
{
    [SerializeField] RangedWeapon[] weapons;
    [SerializeField] float speedBuff = 1;
    public override void DoUpgrade()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].IncreaseProjectileSpeed(speedBuff);
        }
        base.DoUpgrade();
    }
}
