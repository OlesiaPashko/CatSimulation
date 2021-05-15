using System;
using System.Collections.Generic;
using UnityEngine;

public class NeedsFulfill : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float hungerFulfill;
    [SerializeField, Range(0, 100)] private float toiletFulfill;
    [SerializeField, Range(0, 100)] private float communicationFulfill;
    [SerializeField] private DecreaseSpeed[] decreaseSpeeds;

    public Dictionary<InteractableType, float> CurrentFulfill;

    private void Awake()
    {
        CurrentFulfill = new Dictionary<InteractableType, float>()
        {
            {InteractableType.Food, hungerFulfill},
            {InteractableType.Toilet, toiletFulfill},
            {InteractableType.Communication, communicationFulfill}
        };
    }

    private void Update()
    {
        foreach (var decreaseSpeed in decreaseSpeeds)
        {
            var type = decreaseSpeed.type;
            if (CurrentFulfill[type] > 0)
            {
                CurrentFulfill[type] -= decreaseSpeed.speed * Time.deltaTime/60f;
            }
            else
            {
                CurrentFulfill[type] = 0;
            }
            
        }
    }
}

[Serializable]
public struct DecreaseSpeed
{
    public InteractableType type;
    [Range(0, 60)] public float speed;
}