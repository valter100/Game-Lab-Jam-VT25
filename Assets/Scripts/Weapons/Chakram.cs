using UnityEngine;

public class Chakram : Weapon
{
    [SerializeField] int bounceAmount = 2;
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void Fire()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;
            currentAmmo--;

            ammoText.text = currentAmmo.ToString();

            Debug.Log(gameObject.name + " FIRED!");

            if (projectile)
            {
                GameObject spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);

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

                spawnedProjectile.GetComponent<ChakramProjectile>().SetBounces(bounceAmount);

            }

            if (currentAmmo <= 0)
            {
                player.ReturnWeapon(this);
                currentAmmo = startAmmo;
            }
        }

    }

    protected override void ApplyEquipBonus()
    {
        
    }

    protected override void RemoveEquipBonus()
    {
        throw new System.NotImplementedException();
    }

    public void IncreaseBounceAmount(int amount)
    {
        bounceAmount += amount;
    }
}
