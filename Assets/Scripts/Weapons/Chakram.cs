using UnityEngine;

public class Chakram : RangedWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void ApplyEquipBonus()
    {
        
    }

    protected override void RemoveEquipBonus()
    {
        throw new System.NotImplementedException();
    }
}
