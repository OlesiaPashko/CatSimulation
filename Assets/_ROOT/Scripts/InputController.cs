using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log($"{hit.collider.gameObject}");
                    var objectUnderHit = hit.collider.gameObject;
                    if (CanBeTouched(objectUnderHit))
                    {
                        var interactable = objectUnderHit.GetComponent<Interactable>();
                        interactable.Prepare(GetComponent<AutoMove>().GetDirection(transform.position, interactable));
                        var time = GetComponent<AutoMove>().GoToAndInteract(transform.position, interactable);
                    }
                }
            }
        }
    }


    private bool CanBeTouched(GameObject obj)
    {
        return obj.GetComponent<Interactable>() != null;
    }
}