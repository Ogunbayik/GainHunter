using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ANIMATOR_RUN_PARAMETER = "isRunning";
    private const string ANIMATOR_CHEER_PARAMETER = "isCheering";

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
