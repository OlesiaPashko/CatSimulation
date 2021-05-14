
using System.Collections.Generic;
using UnityEngine;

public class NeedsFulfill:MonoBehaviour
{
    public Dictionary<InteractableType, int> CurrentFulfill = new Dictionary<InteractableType, int>()
    {
        {InteractableType.Food, 50},
        {InteractableType.Toilet, 50},
        {InteractableType.Communication, 50}
    };
}
