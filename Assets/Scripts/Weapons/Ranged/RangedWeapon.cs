using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [Header("Ranged Weapon Specific")]
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject shootPoint;
    [SerializeField] protected float projectileSpeed;
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
            currentAmmo--;

            ammoText.text = currentAmmo.ToString();

            if (projectile)
            {
                GameObject spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);

                animator.Play("Fire");

                RaycastHit hit;

                if (Physics.Raycast(player.GetCamera().transform.position, player.GetForward(), out hit, Mathf.Infinity))
                {
                    Vector3 direction = (hit.point - transform.position).normalized;
                    spawnedProjectile.GetComponent<Projectile>().InitializeProjectile(damage, direction, projectileSpeed, damageType);
                }
                else
                {
                    spawnedProjectile.GetComponent<Projectile>().InitializeProjectile(damage, player.GetForward(), projectileSpeed, damageType);
                }

            }

            if (currentAmmo <= 0)
            {
                player.ReturnWeapon(this);
                currentAmmo = startAmmo;
            }
        }
    }
    public void IncreaseProjectileSpeed(float amount)
    {
        projectileSpeed += amount;
    }
}
