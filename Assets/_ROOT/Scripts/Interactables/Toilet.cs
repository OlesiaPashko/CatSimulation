using System;
using UnityEngine;

public class Toilet : Interactable
{
    public override InteractableType Type { get; set; }

    private void Start()
    {
        Type = InteractableType.Toilet;
    }

    public override void OnTouch()
    {
        FindObjectOfType<ToiletCounter>().Count += 10;
        Debug.Log("пісь пісь пісь");
    }
}
