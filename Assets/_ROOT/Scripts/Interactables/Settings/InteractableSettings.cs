
using System.Collections.Generic;

public enum InteractableType
{
    Food,
    Toilet,
    Communication
}

public static class InteractableSettings
{
    public static Dictionary<InteractableType, int> Revenues = new Dictionary<InteractableType, int>()
    {
        {InteractableType.Food, 20},
        {InteractableType.Toilet, 50},
        {InteractableType.Communication, 25}
    };
    
    public static Dictionary<InteractableType, float> InteractionDistance = new Dictionary<InteractableType, float>()
    {
        {InteractableType.Food, 2f},
        {InteractableType.Toilet, 2f},
        {InteractableType.Communication, 2f}
    };
}
