using UnityEngine;

public class ThrowingKnife : Weapon
{
    protected override void ApplyEquipBonus()
    {
        throw new System.NotImplementedException();
    }

    protected override void RemoveEquipBonus()
    {
        throw new System.NotImplementedException();
    }

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
}
