using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Monster", menuName = "ScriptableObject/Monster")]
public class MonsterSO : ScriptableObject
{
    [SerializeField] private string monsterName;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackTimer;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;

    public string GetName()
    {
        return monsterName;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public float GetAttackTimer()
    {
        return attackTimer;
    }


}
