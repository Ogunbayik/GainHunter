using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimation : MonoBehaviour
{
    private const string IS_WALK_ANIMATOR_PARAMETER = "isWalk";
    private const string IS_ATTACK_ANIMATOR_PARAMETER = "isAttack";
    private const string IS_DEATH_ANIMATOR_PARAMETER = "isDeath";
    private const string IS_VICTORY_ANIMATOR_PARAMETER = "isVictory";

    private PetHPManager hPManager;
    private Animator animator;

    [Header("Attack Settings")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform firePoint;
    private void Awake()
    {
        hPManager = GetComponent<PetHPManager>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        hPManager.OnDeath += HPManager_OnDeath;
    }

    private void HPManager_OnDeath(object sender, System.EventArgs e)
    {
        Debug.Log("Death");
        IsDeathAnimation();
    }

    public void IsWalkingAnimation(bool isWalk)
    {
        animator.SetBool(IS_WALK_ANIMATOR_PARAMETER, isWalk);
    }

    public void IsAttackAnimation(bool isAttack)
    {
        animator.SetBool(IS_ATTACK_ANIMATOR_PARAMETER, isAttack);
    }

    public void IsDeathAnimation()
    {
        animator.SetTrigger(IS_DEATH_ANIMATOR_PARAMETER);
    }

    public void IsVictoryAnimation(bool isDance)
    {
        animator.SetBool(IS_VICTORY_ANIMATOR_PARAMETER, isDance);
    }

    public void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        var positionOffset = new Vector3(0, 0.5f, 0f);
        bullet.transform.position = firePoint.position + positionOffset;
        bullet.transform.rotation = transform.rotation;

        var direction = Vector3.forward;
        var movementSpeed = 2f;
        bullet.GetComponent<PetBullet>().Movement(movementSpeed, direction);
    }
}
