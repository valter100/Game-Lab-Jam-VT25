using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MeleeWeapon
{
    [Header("Sword Specific")]
    [SerializeField] float knockbackStrength;

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
        throw new System.NotImplementedException();
    }

    protected override void RemoveEquipBonus()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            animator.Play("Attack");

            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
        }
    }

    public void StartAttack()
    {
        canDamage = true;

        hitEnemies = new List<BaseEnemy>();
    }

    public void EndAttack()
    {
        canDamage = false;
        if (currentAmmo <= 0)
        {
            player.ReturnWeapon(this);
            currentAmmo = startAmmo;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!canDamage)
        {
            return;
        }

        if(other.gameObject.GetComponent<BaseEnemy>() && !hitEnemies.Contains(other.GetComponent<BaseEnemy>()))
        {
            BaseEnemy hitEnemy = other.gameObject.GetComponent<BaseEnemy>();
            hitEnemies.Add(hitEnemy);
            hitEnemy.TakeDamage(damage, other.transform.position, damageType);
        }
    }
}
