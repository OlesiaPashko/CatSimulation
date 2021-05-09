using System.Collections;
using UnityEngine;

public class FoodSource : MonoBehaviour
{
    [SerializeField]
    private Food[] food;
    
    [SerializeField]
    private Vector3 positionToSpawn;
    
    [SerializeField]
    [Range(0, 10)]
    private float delay;

    
    void Start()
    {
        CreateFoodWithDelay();
    }
    
    void ExecuteAfterTime()
    {
        var foodToSpawn = GetRandomFood();
        Instantiate(foodToSpawn, positionToSpawn, Quaternion.identity);
    }

    public void CreateFoodWithDelay()
    {
        Invoke("ExecuteAfterTime", delay);
    }

    private Food GetRandomFood()
    {
        var random = new System.Random();
        var index = random.Next(food.Length);
        return food[index];
    }
}
