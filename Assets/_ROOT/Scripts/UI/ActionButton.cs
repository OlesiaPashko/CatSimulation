
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    private InteractableType type;
    
    [SerializeField]
    private NeedsFulfill needsFulfill;
    
    [SerializeField]
    private ActionButtonSpawner actionButtonSpawner;
    public void DoAction()
    {
        var count = needsFulfill.CurrentFulfill[type];
        var revenue = InteractableSettings.Revenues[type];
        needsFulfill.CurrentFulfill[type] = count + revenue > 100 ? 100 : count + revenue;
        actionButtonSpawner.HideActionButtons();
    }
}
