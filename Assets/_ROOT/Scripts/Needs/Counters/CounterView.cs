using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text countText;

    [SerializeField] private InteractableType type;

    private int count;
    
    public int Count
    {
        get => count;
        set
        {
            count = value;
            countText.text = count.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Count = FindObjectOfType<NeedsFulfill>().CurrentFulfill[type];
    }
}
