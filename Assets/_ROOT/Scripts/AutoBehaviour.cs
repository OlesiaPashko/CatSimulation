using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetBestAction();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GetBestAction()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);
        var bestIncrease = 0;
        Interactable bestInteractable = null;
        foreach (var collider in colliders)
        {
            var interactable = collider.gameObject.GetComponent<Interactable>();
            
            if (interactable != null)
            {
                var currentFulfill = NeedsFulfill.CurrentFulfill[interactable.Type];
                var revenue = interactable.Revenue;
                var futureNeedFulfill = (revenue + currentFulfill) % 100;
                var increase = futureNeedFulfill - currentFulfill;
                if (increase > bestIncrease)
                {
                    bestIncrease = increase;
                    bestInteractable = interactable;
                }
            }
        }
        
        Debug.Log($"bestIncrease = {bestIncrease}");
        Debug.Log($"bestInteractable = {bestInteractable.gameObject}");
    }
}