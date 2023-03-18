using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private Transform body;
    [SerializeField] private float movementSpeed;

    private Vector2 inputVector;
    private Vector3 movementDirection;
    private bool isRunning;
    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        inputVector = gameInput.GetInputVectorNormalized();
        movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movementDirection.magnitude != 0)
        {
            isRunning = true;
            playerAnimation.IsMoving(true);
        }
        else
        {
            isRunning = false;
            playerAnimation.IsMoving(false);
        }

        if (isRunning)
        {
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);

            var rotationSpeed = 5f;
            body.transform.forward = Vector3.Slerp(body.transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
        }
    }

    public void BattleModeActive(Vector3 direction)
    {
        GetComponent<PlayerMovement>().enabled = false;
        body.gameObject.transform.LookAt(direction);
        playerAnimation.IsCheering(true);
    }
    public void BattleModeDeactive()
    {
        GetComponent<PlayerMovement>().enabled = true;
        playerAnimation.IsCheering(false);
    }


    public bool IsRunning()
    {
        return isRunning;
    }
}
