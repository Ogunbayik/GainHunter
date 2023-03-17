using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Patrol,
    Prepare,
    Attack,
    Win,
    Lost
}

public class Monster
{
    [SerializeField] private MonsterSO monsterSO;
    public int level;

    [HideInInspector]
    public string _name;
    [HideInInspector]
    public Sprite sprite;
    [HideInInspector]
    public float movementSpeed;

    private int maxHealth;
    private int damage;

    public Monster(MonsterSO monsterSO, int level)
    {
        this.level = level;
        this._name = monsterSO.GetName();
        this.sprite = monsterSO.GetSprite();
        this.movementSpeed = monsterSO.GetMovementSpeed();
    }


    public int GetDamage()
    {
        damage = monsterSO.GetDamage();
        damage += level * 2;

        return damage;
    }

    public int GetMaxHP()
    {
        maxHealth = monsterSO.GetMaxHealth();
        maxHealth += level * 10;

        return maxHealth;
    }

}
