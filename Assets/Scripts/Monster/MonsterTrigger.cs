using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterTrigger : MonoBehaviour
{
    public event EventHandler OnMonsterTakeDamage;

    private void OnTriggerEnter(Collider other)
    {
        var monsterBullet = other.gameObject.GetComponent<MonsterBullet>();
        if (monsterBullet)
            return;

        var bullet = other.gameObject.GetComponent<PetBullet>();
        if(bullet)
        {
            Destroy(bullet.gameObject);
            OnMonsterTakeDamage?.Invoke(this, EventArgs.Empty);
        }
    }

}
