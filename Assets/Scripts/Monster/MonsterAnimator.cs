using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private const string ANIMATOR_WALK_PARAMETER = "isWalking";
    private const string ANIMATOR_ATTACK_PARAMETER = "isAttacking";
    private const string ANIMATOR_DEATH_PARAMETER = "isDeath";
    private const string ANIMATOR_DANCE_PARAMETER = "isDancing";

    private MonsterUnit monster;
    private MonsterHPManager hpManager;
    private Animator animator;

    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform firePoint;
    private void Awake()
    {
        monster = GetComponent<MonsterUnit>();
        hpManager = GetComponentInParent<MonsterHPManager>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        hpManager.OnMonsterDead += HpManager_OnMonsterDead;
    }

    private void HpManager_OnMonsterDead(object sender, System.EventArgs e)
    {
        DeathAnimation();
    }

    public void WalkAnimation(bool activate)
    {
        animator.SetBool(ANIMATOR_WALK_PARAMETER, activate);
    }

    public void AttackAnimation(bool activate)
    {
        animator.SetBool(ANIMATOR_ATTACK_PARAMETER, activate);
    }

    public void DeathAnimation()
    {
        animator.SetTrigger(ANIMATOR_DEATH_PARAMETER);
    }

    public void DanceAnimation()
    {
        animator.SetTrigger(ANIMATOR_DANCE_PARAMETER);
    }

    public void CreateBullet()
    {
        var speed = 5f;
        var destroyTime = 1f;
        var direction = Vector3.forward;
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<MonsterBullet>().Movement(speed, direction);
        bullet.GetComponent<MonsterBullet>().DestroySelf(destroyTime);
    }

}
