using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject shootPoint;
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float damage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected int startAmmo;
    [SerializeField] protected int currentAmmo;
    [SerializeField] Hand holdingHand;
    [SerializeField] string weaponName;
    [SerializeField] Texture weaponTexture;
    [SerializeField] protected TMP_Text ammoText;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected Animator animator;
    protected float timeSinceLastAttack;
    protected Player player;

    protected void Start()
    {
        currentAmmo = startAmmo;
        player = FindAnyObjectByType<Player>();
    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public virtual void Fire()
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

    public void SetHoldingHand(Hand newHand)
    {
        holdingHand = newHand;
        ammoText = holdingHand.GetAmmoText();
        ammoText.text = startAmmo.ToString();
    }

    protected abstract void ApplyEquipBonus();
    protected abstract void RemoveEquipBonus();

    public Hand GetHoldingHand() => holdingHand;
    public Texture GetTexture() => weaponTexture;
    public string GetName() => weaponName;

    public void IncreaseDamage(float amount)
    {
        damage += amount;
    }

    public void IncreaseProjectileSpeed(float amount)
    {
        projectileSpeed += amount;
    }
}
