using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Animator animator;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float GoToAndInteract(Vector3 playerPosition, Interactable interactable)
    {
        var direction = GetDirection(playerPosition, interactable);
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            var intPos = new Vector3(interactable.transform.position.x, playerPosition.y,
                interactable.transform.position.z);
            transform.rotation = Quaternion.LookRotation(intPos - playerPosition);
        }
        var finalPosition = direction + playerPosition;
        var time = direction.magnitude / speed;
        animator.SetTrigger("StartWalking");
        StartCoroutine(SmoothLerp(time, finalPosition, interactable.Interact));
        return time;
    }
    
    private IEnumerator SmoothLerp(float time, Vector3 finalPosition, Action callback)
    {
        var startingPos = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("StartSitting");
        callback?.Invoke();
    }   
    
    public Vector3 GetDirection(Vector3 playerPosition, Interactable interactable)
    {
        var bestPosition = interactable.transform.position;
        var bestActionPosition = new Vector3(bestPosition.x, playerPosition.y, bestPosition.z);
        var direction = bestActionPosition - playerPosition;
        if (direction.magnitude < interactable.InteractionDistance)
            return Vector3.zero;
        var normalizedDirection = direction.normalized;
        var interactionDistance = interactable.InteractionDistance;
        direction -= normalizedDirection * interactionDistance;
        return direction;
    }
}
