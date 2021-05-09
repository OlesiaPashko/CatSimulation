using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public GameObject particle;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"<color=red>Touch</color>");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log($"<color=red>hit</color>");
                    Debug.Log($"<color=red>{hit.collider.gameObject}</color>");
                    var objectUnderHit = hit.collider.gameObject;
                    //Destroy(hit.collider.gameObject);
                    if (CanBeTouched(objectUnderHit))
                    {
                        Debug.Log("Can be touched");
                        objectUnderHit.GetComponent<Touchable>().OnTouch();
                    }
                }
            }
        }
    }

    private bool CanBeTouched(GameObject obj)
    {
        return obj.GetComponent<Touchable>() != null;
    }
}
