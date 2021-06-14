using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesEnabler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bubbles;

    [SerializeField]
    private SkinnedMeshRenderer cat;
    
    [SerializeField]
    private Material washMaterial;
    
    [SerializeField]
    private Material catMaterial;

    private void Start()
    {
        bubbles.Pause();
    }
    
    public void StartWash()
    {
        bubbles.Play();
        cat.material = washMaterial;
    }

    public void StopWash()
    {
        bubbles.Pause();
        cat.material = catMaterial;
    }
}
