using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Movement.Enable();
    }

    public Vector2 GetInputVectorNormalized()
    {
        var inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
