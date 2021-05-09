using UnityEngine;

public class Food : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<FoodSource>().CreateFoodWithDelay();
    }
}
