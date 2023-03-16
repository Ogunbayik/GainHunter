using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBullet : MonoBehaviour
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
}
