using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetHPManager : MonoBehaviour
{
    public event EventHandler OnDeath;

    [SerializeField] private MonsterHPBar hpBar;

    private PetLevelManager petLevelManager;
    private PetTrigger petTrigger;
    private PetUnit pet;
    
    private int maxHealth;
    private int currentHealth;
    private float healthNormalized;
    private bool isDeath;
    private void Awake()
    {
        petLevelManager = GetComponent<PetLevelManager>();
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
        petLevelManager.OnLevelUp += LevelManager_OnLevelUp;
    }

    private void LevelManager_OnLevelUp(object sender, EventArgs e)
    {
        UpdateHP(pet.GetMaxHealth());
    }

    public void UpdateHP(int hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
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

    public void RefreshHP()
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
