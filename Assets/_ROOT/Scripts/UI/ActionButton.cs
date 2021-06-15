using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    private InteractableType type;

    [SerializeField]
    private ActionButtonSpawner actionButtonSpawner;

    [SerializeField]
    private Shower shower;
    
    [SerializeField]
    private Bed bed;

    public void DoAction()
    {
        if (type == InteractableType.Сleanness)
        {
            shower.Interact();
        }
        else
        {
            bed.Interact();
        }
        actionButtonSpawner.HideActionButtons();
    }
}