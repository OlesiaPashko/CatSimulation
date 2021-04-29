using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private int max;
    
    [SerializeField]
    private int current;
    
    [SerializeField]
    private Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        var fillAmount = (float) current / (float) max;
        mask.fillAmount = fillAmount;
    }
}
