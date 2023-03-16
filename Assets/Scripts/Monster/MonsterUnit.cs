using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnit : MonoBehaviour
{
    public int maxHealth;
    private int damage;
    private MonsterHPManager monsterHPManager;
    private PetHPManager petHPManager;

    private Monster monster;
    [SerializeField] private MonsterSO monsterSO;
    [SerializeField] private int level;

    [SerializeField] private MonsterBattleHud battleHud;

    public States currentState;
    private MonsterAnimator animator;
    private Transform player;
    
    [Header("Patrol Settings")]
    [SerializeField] private float maxDistanceX;
    [SerializeField] private float maxDistanceZ;
    [SerializeField] private LayerMask petLayer;

    private float attackTime;
    [SerializeField] private float startAttackTime;

    private float waitTime;
    private float maximumX;
    private float minimumX;
    private float maximumZ;
    private float minimumZ;
    private Vector3 randomPosition;

    private int petDamage;
    private void Awake()
    {
        animator = GetComponentInChildren<MonsterAnimator>();
        player = FindObjectOfType<PlayerMovement>().transform;
        monsterHPManager = GetComponent<MonsterHPManager>();
    }
    void Start()
    {
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


    private void BattleHudActivate(bool activate)
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
        damage = monsterSO.GetDamage() + level;
        return damage;
    }

    public int GetMaxHp()
    {
        maxHealth = monsterSO.GetMaxHealth() + level;
        return maxHealth;
    }


    public int GetPetDamage()
    {
        return petDamage;
    }

}
