using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PetUnit : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO;

    public enum States
    {
        Follow,
        Prepare,
        Attack,
        Win,
        Lose
    }

    private Monster monster;
    private MonsterHPManager monsterHPManager;

    private PetAnimation animator;
    private PetLevelManager levelManager;
    private PetHPManager hpManager;
    private BoxCollider boxCollider;

    [SerializeField] private MonsterBattleHud battleHud;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerAnimation playerAnimator;

    [Header("Check Settings")]
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask monsterLayer;
    private bool checkSphere;
    [Space]
    [SerializeField] private Transform followPoint;

    private float movementSpeed;
    private float attackTimer;
    private int damage;
    private int maxHealth;

    private int level;
    private int battleExperience;

    private int monsterLevel;
    private int monsterDamage;
    private float monsterDistance;
    private Transform closestMonster;

    private float gameModeDelayTimer;
    private States currentState;

    private void Awake()
    {
        animator = GetComponent<PetAnimation>();
        levelManager = GetComponent<PetLevelManager>();
        hpManager = GetComponent<PetHPManager>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        level = levelManager.GetLevel();
        monster = new Monster(monsterSO, level);
        attackTimer = 1f;
        
        monsterDistance = Mathf.Infinity;
        closestMonster = null;
        currentState = States.Follow;

        hpManager.OnDeath += HpManager_OnDeath;
    }

    private void HpManager_OnDeath(object sender, System.EventArgs e)
    {
        currentState = States.Lose;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case States.Follow: HandleFollow();
                break;
            case States.Prepare: HandlePrepare();
                break;
            case States.Attack: HandleAttack();
                break;
            case States.Win: HandleWin();
                break;
            case States.Lose: Debug.Log("Lose");
                break;
        }

        GetClosestEnemy();
    }

    private void HandleFollow()
    {
        movementSpeed = monsterSO.GetMovementSpeed();
        transform.position = Vector3.Lerp(transform.position, followPoint.position, movementSpeed * Time.deltaTime);
        transform.LookAt(followPoint.position);
        animator.IsWalkingAnimation(true);

        if (closestMonster != null)
        {
            var closestMonster = GetClosestEnemy().GetComponent<MonsterUnit>();
            var distanceBetweenMonster = Vector3.Distance(transform.position, closestMonster.transform.position);
            var lookDistance = 9f;

            if (distanceBetweenMonster <= lookDistance)
            {
                var monster = closestMonster.monster;
                closestMonster.GetBattleHud().SetBattleHud(monster, false);
                closestMonster.BattleHudActivate(true);
            }
            else
            {
                closestMonster.BattleHudActivate(false);
            }
        }

        checkSphere = Physics.CheckSphere(transform.position, checkRadius, monsterLayer);
        if(checkSphere)
        {
            currentState = States.Prepare;
            GameCamera.Instance.BattleCameraActivate();
        }
    }

    private void HandlePrepare()
    {
        player.BattleModeActive(this.gameObject.transform.position);
        var monster = GetClosestEnemy();
        transform.LookAt(monster);
        var distanceBetweenMonster = Vector3.Distance(transform.position, monster.position);

        var prepareSpeed = 0.1f;
        transform.position = Vector3.Lerp(transform.position, monster.position, prepareSpeed * Time.deltaTime);
        animator.IsWalkingAnimation(true);
       
        var attackDistance = 3f;
        if (attackDistance >= distanceBetweenMonster)
        {
            currentState = States.Attack;
        }
    }

    private Transform GetClosestEnemy()
    {
        var currentPosition = transform.position;

        var monsters = FindObjectsOfType<MonsterUnit>();
        foreach (var monster in monsters)
        {
            var distance = Vector3.Distance(monster.transform.position, currentPosition);
            
            if(distance < monsterDistance)
            {
                closestMonster = monster.transform;
                monsterDistance = distance;
                monsterDamage = monster.GetDamage();
                monsterHPManager = monster.GetComponent<MonsterHPManager>();
                monsterLevel = monster.GetLevel();
            }
        }
        monsterDistance = Mathf.Infinity;
        return closestMonster;
    }


    private void HandleAttack()
    {
        battleHud.ActivateBattleHud(true);
        battleHud.SetBattleHud(this.monster, true);
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            animator.IsAttackAnimation(true);
            attackTimer = monsterSO.GetAttackTimer();
        }
        else
        {
            animator.IsWalkingAnimation(false);
            animator.IsAttackAnimation(false);
        }

        if(monsterHPManager.IsDeath())
        {
            battleExperience = 20 * monsterLevel;
            levelManager.AddExperience(battleExperience);
            gameModeDelayTimer = 3f;
            currentState = States.Win;
        }

    }

    public void HandleWin()
    {
        boxCollider.enabled = false;
        animator.IsVictoryAnimation(true);

        gameModeDelayTimer -= Time.deltaTime;

        if(gameModeDelayTimer <= 0)
        {
            boxCollider.enabled = true;
            GameCamera.Instance.VirtualCameraActivate();
            currentState = States.Follow;
            player.BattleModeDeactive();
            animator.IsVictoryAnimation(false);
            battleHud.ActivateBattleHud(false);
            hpManager.RefreshHP();
        }
    }

    public int GetMaxHealth()
    {
        maxHealth = monsterSO.GetMaxHealth() + levelManager.GetLevel() * 2;
        return maxHealth;
    }
    public int GetDamage()
    {
        damage = monsterSO.GetDamage() + levelManager.GetLevel();
        return damage;
    }

    public int GetMonsterDamage()
    {
        return monsterDamage;
    }

    public int GetBattleExperience()
    {
        return battleExperience;
    }
















}
