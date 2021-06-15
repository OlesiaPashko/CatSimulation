using System.Collections;
using UnityEngine;

public class Food : Interactable
{
    [SerializeField]
    private float interactionTime;
    
    public override float InteractionTime => interactionTime;
    
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Food;
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(InteractionTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (FindObjectOfType<FoodSource>() != null)
        {
            FindObjectOfType<FoodSource>().CreateFoodWithDelay();
        }
    }
}
