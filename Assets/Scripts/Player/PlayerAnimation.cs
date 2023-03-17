using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PetHPManager hpManager;

    private const string ANIMATOR_RUN_PARAMETER = "isRunning";
    private const string ANIMATOR_CHEER_PARAMETER = "isCheering";
    private const string ANIMATOR_LOSE_PARAMETER = "isLosing";

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();

        hpManager.OnDeath += HpManager_OnDeath;
    }

    private void HpManager_OnDeath(object sender, System.EventArgs e)
    {
        IsLosing();
    }
    private void IsLosing()
    {
        animator.SetTrigger(ANIMATOR_LOSE_PARAMETER);
    }

    public void IsMoving(bool isActive)
    {
        animator.SetBool(ANIMATOR_RUN_PARAMETER, isActive);
    }

    public void IsCheering(bool isActive)
    {
        animator.SetBool(ANIMATOR_CHEER_PARAMETER, isActive);
    }

}
