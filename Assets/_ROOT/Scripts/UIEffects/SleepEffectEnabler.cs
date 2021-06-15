using System.Collections;
using UnityEngine;

public class SleepEffectEnabler : MonoBehaviour
{
    [SerializeField]
    private SleepEffect sleepEffect;

    [SerializeField]
    private float betweenEffectsPause = 0.5f;

    public void StartEffect()
    {
        InvokeRepeating("CreateEffect", 0, betweenEffectsPause);
    }
    
    public void StopEffect()
    {
        CancelInvoke("CreateEffect");
    }
    
    void CreateEffect() {
        Instantiate(sleepEffect, transform);
    }
}
