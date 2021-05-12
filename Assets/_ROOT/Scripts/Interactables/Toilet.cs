using System;
using System.Collections;
using UnityEngine;

public class Toilet : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Start()
    {
        Type = InteractableType.Toilet;
    }

    public override void Interact()
    {
        FindObjectOfType<ToiletCounter>().Count += 10;
       
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        Debug.Log("Toilet action");
        
        yield return new WaitForSeconds(InteractionTime);
    
    }
}
