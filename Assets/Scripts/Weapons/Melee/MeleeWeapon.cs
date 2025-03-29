using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    [Header("Melee Weapon Specifik")]
    protected bool canDamage;
    protected List<BaseEnemy> hitEnemies = new List<BaseEnemy>();
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
