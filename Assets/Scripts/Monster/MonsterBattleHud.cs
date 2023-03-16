using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterBattleHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image image;

    public void ActivateBattleHud(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetBattleHud(Monster monster, bool isPet)
    {
        if(!isPet)
        {
            nameText.text = $"Name : {monster._name}";
            levelText.text = $"Level : {monster.level}";
            image.sprite = monster.sprite;
        }
        else
        {
            var petLevel = FindObjectOfType<PetLevelManager>();
            nameText.text = $"Name : {monster._name}";
            levelText.text = $"Level : {petLevel.GetLevel()}";
            image.sprite = monster.sprite;
        }
    }
}
