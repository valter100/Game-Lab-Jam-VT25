using UnityEngine;

public class ShieldValueUpgrade :  BaseUpgrade
{
    [SerializeField] Shield weapon;
    [SerializeField] float blockValue = 0.1f;
    public override void DoUpgrade()
    {
        weapon.IncreaseShieldValue(blockValue);
        base.DoUpgrade();
    }
}