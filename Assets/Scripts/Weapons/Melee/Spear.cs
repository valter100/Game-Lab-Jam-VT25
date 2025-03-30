using System.Collections.Generic;
using UnityEngine;

public class Spear : MeleeWeapon
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

    public override void Attack()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            if(holdingHand.GetHandType() == Hand.HandType.Left)
            {
                animator.Play("AttackLeft");
            }
            else
            {
                animator.Play("AttackRight");
            }

            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
        }
    }

    public void StartAttack()
    {
        hitEnemies = new List<BaseEnemy>();
        canDamage = true;
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
        if (!canDamage)
        {
            return;
        }

        if (other.gameObject.GetComponent<BaseEnemy>() && !hitEnemies.Contains(other.GetComponent<BaseEnemy>()))
        {
            BaseEnemy hitEnemy = other.gameObject.GetComponent<BaseEnemy>();
            hitEnemies.Add(hitEnemy);
            hitEnemy.TakeDamage(damage, other.transform.position, damageType);
        }
    }

    protected override void ApplyEquipBonus()
    {
        throw new System.NotImplementedException();
    }

    protected override void RemoveEquipBonus()
    {
        throw new System.NotImplementedException();
    }
}
