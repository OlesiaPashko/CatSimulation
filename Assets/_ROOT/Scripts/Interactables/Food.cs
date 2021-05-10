﻿using System;
using UnityEngine;

public class Food : Interactable
{
    public override InteractableType Type { get; set; }

    private void Start()
    {
        Type = InteractableType.Food;
    }

    public override void Interact()
    {
        FindObjectOfType<HungerCounter>().Count += 20;
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
