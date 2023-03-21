using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterHPManager : MonoBehaviour
{
    public event EventHandler OnMonsterDead;

    [SerializeField] private MonsterHPBar hpBar;

    private MonsterUnit monster;
    private SphereCollider sphereCollider;
    private MonsterTrigger monsterTrigger;

    private int maxHealth;
    private float currentHealth;
    private float healthNormalized;

    private bool isDeath;
    private void Awake()
    {
        monster = GetComponent<MonsterUnit>();
        monsterTrigger = GetComponent<MonsterTrigger>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Start()
    {
        if(!monster.IsBoss())
        {
            maxHealth = monster.GetMaxHp();
            currentHealth = maxHealth;
        }
        else
        {
            maxHealth = monster.GetMaxHp() * 3;
            currentHealth = maxHealth;
        }

        healthNormalized = currentHealth / maxHealth;
        hpBar.SetupHPBar(healthNormalized);

        monsterTrigger.OnMonsterTakeDamage += MonsterTrigger_OnTakeDamage;
    }

    private void MonsterTrigger_OnTakeDamage(object sender, EventArgs e)
    {
        var damage = monster.GetPetDamage();
        TakeDamage(damage);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDeath = true;
            sphereCollider.enabled = false;
            OnMonsterDead?.Invoke(this, EventArgs.Empty);
            DestroySelf();
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            isDeath = false;
            currentHealth -= damage;
            healthNormalized = currentHealth / maxHealth;
            hpBar.SetupHPBar(healthNormalized);
        }
    }

    public void DestroySelf()
    {
        var destroyTime = 1f;
        Destroy(this.gameObject, destroyTime);
    }

    public bool IsDeath()
    {
        return isDeath;
    }


}
