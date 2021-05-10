using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        var rotation = variableJoystick.Horizontal;
        var move = GetMove();
        Vector3 direction = transform.forward * move + transform.right * (rotation/300);
        if (direction == Vector3.zero)
            return;
        transform.position += direction * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private float GetMove()
    {
        var move = variableJoystick.Vertical;
        if (move < 0)
        {
            move = 0.1f;
        }

        return move;
    }
    
    
}