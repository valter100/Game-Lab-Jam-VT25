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
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 3f;
    [Header("Combat")]
    [SerializeField] protected float damage = 3f;
    [Header("Target")]
    [SerializeField] protected GameObject target;
    protected NavMeshAgent agent;
    [Header("Resistance")]
    protected Dictionary<DamageType, float> resistances = new Dictionary<DamageType, float>();

    [SerializeField] Transform textLocation;

    [SerializeField] private List<DamageResistance> resistanceList = new List<DamageResistance>();
    public float CurrentHealth
    {
        get => currentHealth;
        protected set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            if (currentHealth <= 0)
                Die();
        }
    }
    public void SetPlayer(GameObject player)
    {
        this.target = player;
    }

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        InitializeResistances();
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleCombat();

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(Random.Range(10f, 500f), DamageType.Fire);
        }

        if (agent)
        {
            if (agent.speed != moveSpeed)
            {
                agent.speed = moveSpeed;
            }
        }
    }
    public virtual void TakeDamage(float damage, DamageType type)
    {
        float resistance = resistances.ContainsKey(type) ? resistances[type] : 0f;
        float finalDamage = damage * (1f - resistance);
        DamageTextSpawner.Instance.SpawnText(finalDamage, textLocation);

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
        Destroy(gameObject);
    }

    protected abstract void HandleMovement();
    protected abstract void HandleCombat();

    //public abstract void DropCoins();


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
    Fire,
    Water,
    Earth,
    Air
}