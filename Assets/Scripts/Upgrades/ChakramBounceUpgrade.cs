using UnityEngine;

public class ChakramBounceUpgrade : BaseUpgrade
{
    [SerializeField] Chakram weapon;
    [SerializeField] int bounceAmount = 2;
    public override void DoUpgrade()
    {
        weapon.IncreaseBounceAmount(bounceAmount);
        base.DoUpgrade();
    }
}
