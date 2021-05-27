using System.Collections;
using UnityEngine;

public class Bed : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Sleep;
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        Debug.Log("Sleeping");
        
        yield return new WaitForSeconds(InteractionTime);
    }
}

