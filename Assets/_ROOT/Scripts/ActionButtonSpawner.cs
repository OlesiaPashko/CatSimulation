using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonSpawner : MonoBehaviour
{

    [SerializeField] private ActionButton actionButtonPrefab;

    public void SpawnActionButton(Vector2 positionOnScreen, Interactable interactable)
    {
        //var camera = Camera.current;
        var actionButton = Instantiate(actionButtonPrefab, positionOnScreen, Quaternion.identity, transform);
        actionButton.Interactable = interactable;
    }
}
