using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float damage;
    [SerializeField] float projectileSpeed;
    [SerializeField] int startAmmo;
    [SerializeField] int currentAmmo;
    [SerializeField] Hand holdingHand;
    [SerializeField] string weaponName;
    [SerializeField] Texture weaponTexture;
    [SerializeField] TMP_Text ammoText;
    float timeSinceLastAttack;
    Player player;

    protected void Start()
    {
        currentAmmo = startAmmo;
        player = FindAnyObjectByType<Player>();
    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;
            currentAmmo--;

            ammoText.text = currentAmmo.ToString();

            if (currentAmmo <= 0)
            {
                player.ReturnWeapon(this);
                currentAmmo = startAmmo;
            }

            Debug.Log(gameObject.name + " FIRED!");

            if (projectile)
            {
                GameObject spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);

                RaycastHit hit;

                if (Physics.Raycast(player.GetCamera().transform.position, player.GetForward(), out hit, Mathf.Infinity))
                {
                    Vector3 direction = (hit.point - transform.position).normalized;
                    spawnedProjectile.GetComponent<Projectile>().InitializeProjectile(damage, direction, projectileSpeed);
                }
                else
                {
                    spawnedProjectile.GetComponent<Projectile>().InitializeProjectile(damage, player.GetForward(), projectileSpeed);
                }

            }
        }
    }

    public void SetHoldingHand(Hand newHand)
    {
        holdingHand = newHand;
        ammoText = holdingHand.GetAmmoText();
        ammoText.text = startAmmo.ToString();
    }

    public void SetAmmoText()
    {

    }

    public Hand GetHoldingHand() => holdingHand;
    public Texture GetTexture() => weaponTexture;
    public string GetName() => weaponName;
}
