using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Food;
    }

    public override void Interact()
    {
        var counter = FindObjectOfType<HungerCounter>();
        var count = counter.Count;
        var revenue = InteractableSettings.Revenues[Type];
        counter.Count = count + revenue > 100 ? 100 : count + revenue;
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        Debug.Log("Omnomnom");
        
        yield return new WaitForSeconds(InteractionTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (FindObjectOfType<FoodSource>() != null)
        {
            FindObjectOfType<FoodSource>().CreateFoodWithDelay();
        }
    }
}
