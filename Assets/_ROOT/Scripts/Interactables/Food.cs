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

    private void Start()
    {
        Type = InteractableType.Food;
    }

    public override void Interact()
    {
        FindObjectOfType<HungerCounter>().Count += 20;
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
