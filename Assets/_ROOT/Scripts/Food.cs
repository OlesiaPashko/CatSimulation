using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnDestroy()
    {
        FindObjectOfType<FoodSource>().CreateFoodWithDelay();
    }
}
