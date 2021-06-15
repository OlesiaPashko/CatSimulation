using System.Collections;
using UnityEngine;

public class Toy : Interactable
{
    [SerializeField]
    private float interactionTime;

    [SerializeField]
    private FunEffectSpawner funEffectSpawner;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Fun;
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        funEffectSpawner.StartEffect();
        yield return new WaitForSeconds(InteractionTime);
        funEffectSpawner.StopEffect();
    }
}