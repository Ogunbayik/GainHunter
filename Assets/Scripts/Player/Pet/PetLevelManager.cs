using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetLevelManager : MonoBehaviour
{
    [SerializeField] private int currentExperience;
    [SerializeField] private int maxExperience;

    private int startLevel = 1;
    private int level;

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
            currentExperience = 0;
            maxExperience *= 2;
            level++;
        }
    }

    public int GetLevel()
    {
        return level;
    }



}
