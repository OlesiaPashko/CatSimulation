using UnityEngine;

public class FunEffectSpawner : MonoBehaviour
{
    [SerializeField]
    private FunEffect[] funEffectVariants;
    
    [SerializeField]
    private float betweenEffectsPause = 0.3f;
    
    [SerializeField]
    private float spawnRadius = 500f;
    
    public void StartEffect()
    {
        InvokeRepeating("CreateEffect", 0, betweenEffectsPause);
    }
    
    public void StopEffect()
    {
        CancelInvoke("CreateEffect");
    }
    
    void CreateEffect()
    {
        var point = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
        var random = new System.Random();
        var effect = funEffectVariants[random.Next(0, funEffectVariants.Length - 1)];
        var cam = Camera.main;
        Instantiate(effect, cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)) + point, Quaternion.identity, transform);
    }
}
