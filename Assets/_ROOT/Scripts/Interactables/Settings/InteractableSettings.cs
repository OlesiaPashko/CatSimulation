
using System.Collections.Generic;

public enum InteractableType
{
    Toilet,
    Food,
    Communication,
    Sleep,
    Сleanness,
    Fun
}

public static class InteractableSettings
{
    public static Dictionary<InteractableType, int> Revenues = new Dictionary<InteractableType, int>()
    {
        {InteractableType.Food, 50},
        {InteractableType.Toilet, 100},
        {InteractableType.Communication, 50},
        {InteractableType.Sleep, 50},
        {InteractableType.Сleanness, 50},
        {InteractableType.Fun, 50},
    };
    
    public static Dictionary<InteractableType, float> InteractionDistance = new Dictionary<InteractableType, float>()
    {
        {InteractableType.Food, 0.25f},
        {InteractableType.Toilet, -0.05f},
        {InteractableType.Communication, 0.5f},
        {InteractableType.Sleep, 0f},
        {InteractableType.Сleanness, 0f},
        {InteractableType.Fun, 50},
    };
}
