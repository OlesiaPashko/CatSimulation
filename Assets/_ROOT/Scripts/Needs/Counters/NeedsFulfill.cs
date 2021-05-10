
using System.Collections.Generic;

public static class NeedsFulfill
{
    public static Dictionary<InteractableType, int> CurrentFulfill = new Dictionary<InteractableType, int>()
    {
        {InteractableType.Food, 50},
        {InteractableType.Toilet, 50},
        {InteractableType.Communication, 50}
    };
}
