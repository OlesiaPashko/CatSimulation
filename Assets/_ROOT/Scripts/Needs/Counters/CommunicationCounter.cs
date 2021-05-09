using TMPro;
using UnityEngine;

public class CommunicationCounter : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text countText;

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
        Count = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
