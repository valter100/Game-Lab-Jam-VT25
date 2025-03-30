using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float damage;
    [SerializeField] protected int startAmmo;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected Hand holdingHand;
    [SerializeField] string weaponName;
    [SerializeField] string weaponDescription;
    [SerializeField] Texture weaponTexture;
    [SerializeField] protected TMP_Text ammoText;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected Animator animator;
    protected float timeSinceLastAttack;
    protected Player player;

    public GameObject upgradesHolder;
    protected void Start()
    {
        currentAmmo = startAmmo;
        timeSinceLastAttack = timeBetweenAttacks;
        player = FindAnyObjectByType<Player>();
    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void SetHoldingHand(Hand newHand)
    {
        holdingHand = newHand;
        ammoText = holdingHand.GetAmmoText();
        ammoText.text = startAmmo.ToString();
    }

    protected abstract void ApplyEquipBonus();
    protected abstract void RemoveEquipBonus();
    public abstract void Attack();

    public Hand GetHoldingHand() => holdingHand;
    public Texture GetTexture() => weaponTexture;
    public string GetName() => weaponName;
    public string GetDescription() => weaponDescription;

    public void IncreaseDamage(float amount)
    {
        damage += amount;
    }
}
