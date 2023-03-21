using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PetLevelManager : MonoBehaviour
{
    public event EventHandler OnLevelUp;

    [SerializeField] private GameObject levelUpEffect;

    private PetHPManager hpManager;
    private PetUnit pet;

    [SerializeField] private GameObject levelUpText;
    [SerializeField] private GameObject experiencePopUp;
    [SerializeField] private Transform experienceCanvas;

    private int currentExperience;
    [SerializeField] private int maxExperience;

    [SerializeField] private int startLevel;
    private int level;
    private void Awake()
    {
        hpManager = GetComponent<PetHPManager>();
        pet = GetComponent<PetUnit>();
    }

    private void Start()
    {
        level = startLevel;
    }

    public void AddExperience(int experience)
    {
        currentExperience += experience;
        UpgradeLevel();
    }

    private void UpgradeLevel()
    {
        if(currentExperience >= maxExperience)
        {
            var effect = Instantiate(levelUpEffect, transform.position, Quaternion.identity);
            var destroyTime = 3f;
            Destroy(effect, destroyTime);
            currentExperience = 0;
            maxExperience *= 2;
            level++;
            CreatePopUp(true);

            OnLevelUp?.Invoke(this, EventArgs.Empty);
            return;
        }

        CreatePopUp(false);
    }

    private void CreatePopUp(bool isLevelUp)
    {
        var experience = pet.GetBattleExperience();
        var popUp = Instantiate(experiencePopUp, experienceCanvas);

        if (!isLevelUp)
        {
            popUp.GetComponent<PopUpExperience>().SetPopUp(experience.ToString());
        }
        else
        {
            popUp.GetComponent<PopUpExperience>().SetPopUp($"Level UP!");
        }
        popUp.GetComponent<PopUpExperience>().enabled = true;
    }

    public int GetLevel()
    {
        return level;
    }



}
