using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    [SerializeField] private Image filledBar;

    public void SetupHPBar(float amount)
    {
        filledBar.fillAmount = amount;
    }

}
