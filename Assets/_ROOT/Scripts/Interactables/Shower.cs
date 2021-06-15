using System.Collections;
using UnityEngine;

public class Shower : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }
    
    [SerializeField]
    private BubblesEnabler bubblesEnabler;

    private void Awake()
    {
        Type = InteractableType.Сleanness;
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        bubblesEnabler.StartWash();
        yield return new WaitForSeconds(InteractionTime);
        bubblesEnabler.StopWash();
    }
}