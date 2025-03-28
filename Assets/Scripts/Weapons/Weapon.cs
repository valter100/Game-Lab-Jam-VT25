using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float damage;
    [SerializeField] int startAmmo;
    [SerializeField] int currentAmmo;
    [SerializeField] Hand holdingHand;
    float timeSinceLastAttack;
    Player player;

    private void Start()
    {
        currentAmmo = startAmmo;
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;
            currentAmmo--;

            if (currentAmmo <= 0)
            {
                player.ReturnWeapon(this);
                currentAmmo = startAmmo;
            }

            Debug.Log(gameObject.name + " FIRED!");

            if (projectile)
            {
                GameObject spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
                spawnedProjectile.GetComponent<Projectile>().InitializeProjectile(damage, player.transform.forward);
            }
        }
    }

    public void SetHoldingHand(Hand newHand)
    {
        holdingHand = newHand;
    }

    public Hand GetHoldingHand() => holdingHand;
}
