using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO;
    [SerializeField] private MonsterBattleHud battleHud;

    public Monster monster;
    private MonsterAnimator animator;
    private MonsterHPManager monsterHPManager;

    private int maxHealth;
    private int damage;

    private int level;

    [Header("Patrol Settings")]
    [SerializeField] private float maxDistanceX;
    [SerializeField] private float maxDistanceZ;
    [SerializeField] private LayerMask petLayer;

    private float attackTime;
    private float waitTime;
    private float maximumX;
    private float minimumX;
    private float maximumZ;
    private float minimumZ;
    private Vector3 randomPosition;

    private int petDamage;
    private PetHPManager petHPManager;

    public States currentState;
    [SerializeField] private bool isBoss;
    private void Awake()
    {
        animator = GetComponentInChildren<MonsterAnimator>();
        monsterHPManager = GetComponent<MonsterHPManager>();
    }
    void Start()
    {
        SetMonsterLevel();
        monster = new Monster(monsterSO, level);
        currentState = States.Patrol;

        maximumX = transform.position.x + maxDistanceX;
        minimumX = transform.position.x - maxDistanceX;
        maximumZ = transform.position.z + maxDistanceZ;
        minimumZ = transform.position.z - maxDistanceZ;

        waitTime = 1f;

        randomPosition = GetRandomPosition();
        battleHud.gameObject.SetActive(false);
    }

    private void SetMonsterLevel()
    {
        if (!isBoss)
        {
            var randomIndex = Random.Range(0, 100);
            if (0 < randomIndex && randomIndex < 60)
            {
                level = 1;
            }
            else if (60 <= randomIndex && randomIndex < 90)
            {
                level = 2;
            }
            else if (randomIndex <= 90)
            {
                level = 3;
            }
        }
        else
        {
            level = 5;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrolling();
                break;
            case States.Prepare:
                Preparing();
                break;
            case States.Attack:
                Attacking();
                break;
            case States.Win:  Debug.Log("Destroy the Pet");
                Won();
                break;
            case States.Lost: Debug.Log("Player Pet destroyed you and gain Experience");
                Lose();
                break;
        }
    }

    private void Patrolling()
    {
        var distanceToPoint = Vector3.Distance(transform.position, randomPosition);
        var reachDistance = 0.1f;

        if (distanceToPoint > reachDistance)
        {
            var movementSpeed = monster.movementSpeed;
            transform.position = Vector3.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);

            var lookDirection = (randomPosition - transform.position);
            var rotationSpeed = 5f;
            transform.forward = Vector3.Slerp(transform.forward, lookDirection, rotationSpeed * Time.deltaTime);

            animator.WalkAnimation(true);
        }
        else
        {
            waitTime -= Time.deltaTime;
            animator.WalkAnimation(false);
            if (waitTime <= 0)
            {
                randomPosition = GetRandomPosition();
                waitTime = monsterSO.GetAttackTimer(); 
            }
        }

        CheckPlayerPet();
    }

    private void CheckPlayerPet()
    {
        var sphereRadius = 5f;
        var checkSphere = Physics.CheckSphere(transform.position, sphereRadius, petLayer);

        if (checkSphere)
        {
            animator.WalkAnimation(true);
            currentState = States.Prepare;
        }
    }

    private void Preparing()
    {
        var pet = FindObjectOfType<PetUnit>();
        petDamage = pet.GetDamage();
        petHPManager = pet.GetComponent<PetHPManager>();
        var lookDirection = (pet.transform.position - transform.position);
        var rotationSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, lookDirection, rotationSpeed * Time.deltaTime);

        var prepareSpeed = monster.movementSpeed / 2;
        transform.position = Vector3.MoveTowards(transform.position, pet.transform.position, prepareSpeed * Time.deltaTime);

        var distanceToPlayer = Vector3.Distance(transform.position, pet.transform.position);
        var attackDistance = 3f;

        if (distanceToPlayer < attackDistance)
        {
            currentState = States.Attack;
        }
    }

    private void Attacking()
    {
        animator.AttackAnimation(false);
        BattleHudActivate(true);
        battleHud.SetBattleHud(this.monster,false);

        attackTime -= Time.deltaTime;

        if (attackTime <= 0)
        {
            animator.AttackAnimation(true);
            attackTime = monsterSO.GetAttackTimer();
        }

        if(petHPManager.IsDeath())
        {
            currentState = States.Win;
        }
        
        if(monsterHPManager.IsDeath())
        {
            currentState = States.Lost;
        }

    }

    private void Won()
    {
        BattleHudActivate(false);
        animator.DanceAnimation();
    }

    private void Lose()
    {
        BattleHudActivate(false);
    }


    public void BattleHudActivate(bool activate)
    {
        battleHud.gameObject.SetActive(activate);
    }

    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(minimumX, maximumX);
        var randomZ = Random.Range(minimumZ, maximumZ);
        var positionY = 0f;

        var randomPoint = new Vector3(randomX, positionY, randomZ);

        return randomPoint;
    }

    public int GetDamage()
    {
        damage = monsterSO.GetDamage() + level * 2;
        

        return damage;
    }

    public int GetMaxHp()
    {
        maxHealth = monsterSO.GetMaxHealth() + level * 5;
        return maxHealth;
    }

    public int GetPetDamage()
    {
        return petDamage;
    }

    public int GetLevel()
    {
        return level;
    }

    public MonsterBattleHud GetBattleHud()
    {
        return battleHud;
    }

    public bool IsBoss()
    {
        return isBoss;
    }

}
