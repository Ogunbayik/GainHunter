using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpExperience : MonoBehaviour
{
    private PetUnit pet;

    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private float movementSpeed;

    private int experience;
    private void Awake()
    {
        pet = FindObjectOfType<PetUnit>();

        experience = pet.GetBattleExperience();
        experienceText.text = experience.ToString();
    }

    private void Update()
    {
        Movement();
    }

    public void SetPopUp(string text)
    {
        experienceText.text = text;
    }

    private void Movement()
    {
        var destroyTime = 0.75f;
        experienceText.alpha -= destroyTime * Time.deltaTime;
        experienceText.rectTransform.position += Vector3.up * movementSpeed * Time.deltaTime;

        if (experienceText.alpha <= 0)
            Destroy(this.gameObject);
    }
}
