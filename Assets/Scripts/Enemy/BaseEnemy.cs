using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static BaseEnemy;
using Random = UnityEngine.Random;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    [SerializeField] int expGiven;
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 3f;
    [Header("Combat")]
    [SerializeField] protected float damage = 3f;
    [Header("Target")]
    [SerializeField] protected GameObject target;



    [Header("Resistance")]
    protected Dictionary<DamageType, float> resistances = new Dictionary<DamageType, float>();

    [SerializeField] private List<DamageResistance> resistanceList = new List<DamageResistance>();

    public Dictionary<DamageType, float> Resistances => resistances;
    public float CurrentHealth
    {
        get => currentHealth;
        protected set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

            if (currentHealth <= 0)
            {
                
                Die();
            }
        }
    }
    public void SetPlayer(GameObject player)
    {
        this.target = player;
    }

    public virtual void ReduceSpeed()
    {

    }

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;

        InitializeResistances();
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleCombat();
    }
    public virtual void TakeDamage(float damage, Vector3 position, DamageType type)
    {
        float resistance = resistances.ContainsKey(type) ? resistances[type] : 0f;
        float finalDamage = damage * (1f - resistance);
        DamageTextSpawner.Instance.SpawnText(finalDamage, position, type);
        ReduceSpeed();
        CurrentHealth -= finalDamage;
    }

    public virtual void TakeDamage(float damage, DamageType type, Vector3 position, bool crit)
    {
        float resistance = resistances.ContainsKey(type) ? resistances[type] : 0f;
        float finalDamage = damage * (1f - resistance);
        DamageTextSpawner.Instance.SpawnText(finalDamage, position, type);
        ReduceSpeed();
        CurrentHealth -= finalDamage;
    }

    protected virtual void InitializeResistances()
    {
        for (int i = 0; i < resistanceList.Count; i++)
        {
            resistances[resistanceList[i].damageType] = resistanceList[i].resistanceValue;
        }
    }


    protected virtual void Die()
    {
        CoinManager.Instance.SpawnCoin(transform.position);
        target.GetComponent<Player>().GainExp(expGiven);
        Destroy(gameObject);
    }

    protected abstract void HandleMovement();
    protected abstract void HandleCombat();

    //public abstract void DropCoins();

    public virtual void UpdateMoveSpeed(float percentage)
    {
        moveSpeed *= percentage;
    }

    public virtual void UpdateDamage(float percentage)
    {
        damage *= percentage;
    }

    public virtual void UpdateHealth(float percentage)
    {
        maxHealth *= percentage;
        currentHealth = maxHealth;
    }
    public void IncreaseScaling(float amount)
    {
        maxHealth *= amount;

        currentHealth *= maxHealth;

        damage *= amount;
    }

}
[Serializable]
public class DamageResistance
{
    public DamageType damageType;
    [Range(-1f, 1f)] public float resistanceValue;
}

public enum DamageType
{
    None,
    Fire,
    Water,
    Earth,
    Air
}