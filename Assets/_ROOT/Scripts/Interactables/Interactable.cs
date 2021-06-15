using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public int Revenue => InteractableSettings.Revenues[Type];
    public float InteractionDistance => InteractableSettings.InteractionDistance[Type];
    
    public abstract float InteractionTime { get; }

    public float GetNeedFulfill()
    {
        return FindObjectOfType<NeedsFulfill>().CurrentFulfill[Type];
    }
    public abstract InteractableType Type { get; set; }

    public virtual void Interact()
    {
        var fulfill= FindObjectOfType<NeedsFulfill>();
        var count = fulfill.CurrentFulfill[Type];
        var revenue = InteractableSettings.Revenues[Type];
        fulfill.CurrentFulfill[Type] = count + revenue > 100 ? 100 : count + revenue;
    }
    public virtual void Prepare(Vector3 direction){}
}
