using System;
using System.Collections;
using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        var bestAction = GetBestAction();
        GoTo(bestAction);
    }

    private void GoTo(Interactable bestAction)
    {
        var finalPosition = bestAction.transform.position;
        transform.rotation = Quaternion.LookRotation(finalPosition - transform.position);
        StartCoroutine(SmoothLerp(3f, finalPosition, bestAction.Interact));
    }
    
    private IEnumerator SmoothLerp (float time, Vector3 finalPosition, Action callback)
    {
        var startingPos  = transform.position;
        var elapsedTime = 0f;
         
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        callback();
    }

    private Interactable GetBestAction()
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

        return bestInteractable;
    }
}