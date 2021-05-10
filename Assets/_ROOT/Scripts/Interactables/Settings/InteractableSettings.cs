
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
        {InteractableType.Toilet, 10},
        {InteractableType.Communication, 25}
    };
}
