using UnityEngine;

public class BaseDamageUpgrade : BaseUpgrade
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] float damageBuff = 1;
    public override void DoUpgrade()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].IncreaseDamage(damageBuff);
        }
        base.DoUpgrade();
    }
}
