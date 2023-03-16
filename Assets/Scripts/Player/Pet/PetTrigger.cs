using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetTrigger : MonoBehaviour
{
    public event EventHandler OnPetTakeDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PetBullet>())
            return;

        var bullet = other.gameObject.GetComponent<MonsterBullet>();
        if(bullet)
        {
            Destroy(bullet.gameObject);
            OnPetTakeDamage?.Invoke(this, EventArgs.Empty);
        }
    }

}
