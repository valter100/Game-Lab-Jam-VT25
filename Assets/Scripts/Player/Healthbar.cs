using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    Image image;

    public void Start()
    {
        image = GetComponent<Image>();
        health.OnDamage.AddListener(UpdateHealth);
    }

    public void UpdateHealth(float damage)
    {
        image.fillAmount = health.CurrentHealth / health.MaxHealth;
    }
}
