using System;
using System.Collections.Generic;
using UnityEngine;

public class NeedsFulfill : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float hungerFulfill;
    [SerializeField, Range(0, 100)] private float toiletFulfill;
    [SerializeField, Range(0, 100)] private float communicationFulfill;
    [SerializeField] private DecreaseSpeed[] decreaseSpeeds;

    private Dictionary<InteractableType, float> currentFulfill;
    
    public Dictionary<InteractableType, float> CurrentFulfill
    {
        get => currentFulfill;
        set
        {
            currentFulfill = value;
        }
    }

    public float Happiness { get; set; }

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
                CurrentFulfill[type] -= decreaseSpeed.speed * Time.deltaTime / 60f;
            }
            else
            {
                CurrentFulfill[type] = 0;
            }
        }
        Happiness = GetHappiness();
    }

    public Dictionary<InteractableType, float> EmulateTimeForNeedsFulfill(
        Dictionary<InteractableType, float> needsFulfill, float time)
    {
        var newNeedsFulfill = new Dictionary<InteractableType, float>(needsFulfill);
        foreach (var decreaseSpeed in decreaseSpeeds)
        {
            var type = decreaseSpeed.type;
            newNeedsFulfill[type] -= decreaseSpeed.speed * time / 60f;
            if (newNeedsFulfill[type] < 0)
            {
                newNeedsFulfill[type] = 0;
            }
        }

        return newNeedsFulfill;
    }

    private float GetHappiness()
    {
        float happiness = 0;
        float maxHappiness = 0;
        foreach (var fulFill in currentFulfill)
        {
            var feature = CharacterSettings.GetFeatureForNeed(fulFill.Key);
            var characterMultiplayer = CharacterSettings.Features[feature];
            var currentFulfillOfNeed = fulFill.Value;
            var impactOnHappiness = currentFulfillOfNeed * characterMultiplayer;
            maxHappiness += 100f * characterMultiplayer;
            happiness += impactOnHappiness;
        }

        return (happiness / maxHappiness) * 100f;
    }
}

[Serializable]
public struct DecreaseSpeed
{
    public InteractableType type;
    [Range(0, 60)] public float speed;
}