using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonSpawner : MonoBehaviour
{

    [SerializeField] private ActionButton[] actionButtons;

    public void ShowActionButtons()
    {
        foreach (var actionButton in actionButtons)
        {
            actionButton.gameObject.SetActive(true);
        }
    }
    
    public void HideActionButtons()
    {
        foreach (var actionButton in actionButtons)
        {
            actionButton.gameObject.SetActive(false);
        }
    }
}
