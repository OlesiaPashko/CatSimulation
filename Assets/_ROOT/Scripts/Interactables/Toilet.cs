using System;
using System.Collections;
using UnityEngine;

public class Toilet : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Toilet;
    }

    public override void Interact()
    {
        var counter = FindObjectOfType<ToiletCounter>();
        var count = counter.Count;
        var revenue = InteractableSettings.Revenues[Type];
        counter.Count = count + revenue > 100 ? 100 : count + revenue;
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        Debug.Log("Toilet action");
        
        yield return new WaitForSeconds(InteractionTime);
    
    }
}
