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
    
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        bubbles.Pause();
    }
    
    public void StartWash()
    {
        animator.SetTrigger("StartSitting");
        bubbles.Play();
        cat.material = washMaterial;
    }

    public void StopWash()
    {
        bubbles.Stop();
        cat.material = catMaterial;
    }
}
