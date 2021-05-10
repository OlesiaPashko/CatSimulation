using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public int Revenue => InteractableSettings.Revenues[Type];

    public int GetNeedFulfill()
    {
        return NeedsFulfill.CurrentFulfill[Type];
    }
    public abstract InteractableType Type { get; set; }
    public abstract void Interact();
}
