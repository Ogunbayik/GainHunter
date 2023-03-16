using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetHPManager : MonoBehaviour
{
    public event EventHandler OnDeath;

    [SerializeField] private MonsterHPBar hpBar;

    private PetTrigger petTrigger;
    private PetUnit pet;
    
    private int maxHealth;
    private int currentHealth;
    private float healthNormalized;
    private bool isDeath;
    private void Awake()
    {
        petTrigger = GetComponent<PetTrigger>();
        pet = GetComponent<PetUnit>();
    }

    void Start()
    {
        maxHealth = pet.GetMaxHealth();
        currentHealth = maxHealth;
        healthNormalized = (float)currentHealth / maxHealth;
        hpBar.SetupHPBar(healthNormalized);

        petTrigger.OnPetTakeDamage += PetTrigger_OnTakeDamage;
    }

    private void PetTrigger_OnTakeDamage(object sender, System.EventArgs e)
    {
        var damage = pet.GetMonsterDamage();
        TakeDamage(damage);
        isDeath = false;

        if(currentHealth <= 0)
        {
            isDeath = true;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthNormalized = (float)currentHealth / maxHealth;
            hpBar.SetupHPBar(healthNormalized);
        }
    }

    public void RestartHP()
    {
        currentHealth = maxHealth;
        healthNormalized = (float)currentHealth / maxHealth;
        hpBar.SetupHPBar(healthNormalized);
    }

    public bool IsDeath()
    {
        return isDeath;
    }

}
