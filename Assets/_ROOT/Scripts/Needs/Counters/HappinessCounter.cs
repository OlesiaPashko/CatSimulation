using TMPro;
using UnityEngine;

public class HappinessCounter : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text countText;

    [SerializeField] private NeedsFulfill needsFulfill;

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
    
    void Start()
    {
        Count = Mathf.RoundToInt(needsFulfill.Happiness);
    }

    private void LateUpdate()
    {
        Count = Mathf.RoundToInt(needsFulfill.Happiness);
    }
}
