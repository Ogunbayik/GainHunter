using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    public PetHPManager pet;

    private const string ANIMATOR_GAMEOVER_PARAMETER = "GameOver";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        pet.OnDeath += Pet_OnDeath;
    }

    private void Pet_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ANIMATOR_GAMEOVER_PARAMETER);
    }
}
