
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public Interactable Interactable { get; set; }
    public void DoAction()
    {
        Interactable.Interact();
        
    }
}
