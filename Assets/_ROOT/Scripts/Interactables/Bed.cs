using System;
using System.Collections;
using UnityEngine;

public class Bed : Interactable
{
    [SerializeField]
    private float interactionTime;

    [SerializeField]
    private Transform armatureRoot;

    [SerializeField]
    private SleepEffectEnabler sleepEffectEnabler;

    [SerializeField]
    private float speed = 1f;

    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private bool IsGettingDown;
    private bool IsGettingUp;

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
        armatureRoot.rotation =  Quaternion.Euler(0, 0, 90);
        armatureRoot.position += new Vector3(0, 0.1f, 0);
        sleepEffectEnabler.StartEffect();
        yield return new WaitForSeconds(InteractionTime);
        armatureRoot.rotation =  Quaternion.Euler(0, 0, 0);
        armatureRoot.position -= new Vector3(0, 0.1f, 0);
        sleepEffectEnabler.StopEffect();
        yield return new WaitForSeconds(2f);
    }
}

