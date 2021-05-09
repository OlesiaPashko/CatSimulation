using UnityEngine;

public class Food : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        FindObjectOfType<HungerCounter>().Count += 20;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<FoodSource>().CreateFoodWithDelay();
    }
}
