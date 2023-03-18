using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;

    void Update()
    {
        Movement(speed, direction);
    }

    public void Movement(float movementSpeed, Vector3 movementDirection)
    {
        speed = movementSpeed;
        direction = movementDirection;
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }

    public void DestroySelf(float destroyTime)
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PetBullet>())
            return;
    }
}
