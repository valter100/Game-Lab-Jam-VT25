using UnityEngine;

public class Boomerang : RangedWeapon
{
    [SerializeField] bool canThrow = true;
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
        if (canThrow)
        {
            transform.localScale = Vector3.zero;

            currentAmmo--;

            ammoText.text = currentAmmo.ToString();

            if (projectile)
            {
                BoomerangProjectile spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation).GetComponent<BoomerangProjectile>();

                RaycastHit hit;

                if (Physics.Raycast(player.GetCamera().transform.position, player.GetForward(), out hit, Mathf.Infinity))
                {
                    Vector3 direction = (hit.point - transform.position).normalized;
                    spawnedProjectile.InitializeProjectile(damage, direction, projectileSpeed, damageType);
                    spawnedProjectile.SetFiredWeapon(this);
                }
                else
                {
                    spawnedProjectile.InitializeProjectile(damage, player.GetForward(), projectileSpeed, damageType);
                }

            }

            canThrow = false;
        }
    }

    public void SetCanThrowTrue()
    {
        canThrow = true;
    }

    public void SetCanThrow(bool state)
    {
        canThrow = state;
    }

    public void PlayGrabAnimation()
    {
        if (currentAmmo <= 0)
        {
            player.ReturnWeapon(this);
            canThrow = true;
            currentAmmo = startAmmo;
        }
        else
        {
            animator.Play("Grab");
        }
        
        transform.localScale = Vector3.one;

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
