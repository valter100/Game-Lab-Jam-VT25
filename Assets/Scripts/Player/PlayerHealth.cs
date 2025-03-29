using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float currentHealth = 1f;
    [SerializeField] float maxHealth = 1f;
    [SerializeField] float invincibility = 1f;
    float invincibilityReset = 1f;
    public UnityEvent<float> OnDamage;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        invincibility -= Time.deltaTime;
    }

    public void TakeDamage(float amount)
    {
        if (invincibility > 0) { return; }
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            Die();
            return;
        }
        OnDamage.Invoke(amount);
        invincibility = invincibilityReset;
    }

    public void Die()
    {

    }

}
