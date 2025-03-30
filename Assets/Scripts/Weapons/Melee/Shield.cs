using System.Collections.Generic;
using UnityEngine;

public class Shield : MeleeWeapon
{
    public float shieldValue = 0.5f;
    
    bool isGuarding;
    // Start is called
    // once before the first execution of Update after the MonoBehaviour is created
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

            if (holdingHand.GetHandType() == Hand.HandType.Left)
            {
                animator.Play("DefendLeft");
            }
            else
            {
                animator.Play("DefendRight");
            }

            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
        }
    }

    public void StartDefense()
    {
        isGuarding = true;
    }

    public void EndDefense()
    {
        isGuarding = false;

        if (currentAmmo <= 0)
        {
            player.ReturnWeapon(this);
            currentAmmo = startAmmo;
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

    public void IncreaseShieldValue(float amount)
    {
        shieldValue -= amount;
    }
}
