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
        bestAction.Prepare();
        GoTo(bestAction);
    }

    private void GoTo(Interactable bestAction)
    {
        var direction = GetDirection(bestAction);
        transform.rotation = Quaternion.LookRotation(direction);
        var finalPosition = direction + transform.position;
        StartCoroutine(SmoothLerp(3f, finalPosition, bestAction.Interact));
    }

    private Vector3 GetDirection(Interactable bestAction)
    {
        var bestPosition = bestAction.transform.position;
        var bestActionPosition = new Vector3(bestPosition.x, transform.position.y, bestPosition.z);
        var direction = bestActionPosition - transform.position;
        var normalizedDirection = direction.normalized;
        var interactionDistance = bestAction.InteractionDistance;
        direction -= normalizedDirection * interactionDistance;
        return direction;
    }

    private IEnumerator SmoothLerp(float time, Vector3 finalPosition, Action callback)
    {
        var startingPos = transform.position;
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
                Debug.Log($"Interactable = {interactable.gameObject}");
                var currentFulfill = NeedsFulfill.CurrentFulfill[interactable.Type];
                Debug.Log($"currentFulfill = {currentFulfill}");
                var revenue = interactable.Revenue;
                Debug.Log($"revenue = {revenue}");
                var futureNeedFulfill = revenue + currentFulfill > 100
                    ? 100 - revenue + currentFulfill
                    : revenue + currentFulfill;
                Debug.Log($"futureNeedFulfill = {futureNeedFulfill}");

                var increase = futureNeedFulfill - currentFulfill;
                Debug.Log($"increase = {increase}");

                if (increase > bestIncrease)
                {
                    Debug.Log("better");
                    bestIncrease = increase;
                    bestInteractable = interactable;
                }
            }
        }

        Debug.Log(bestInteractable.gameObject);
        Debug.Log(bestIncrease);
        return bestInteractable;
    }
}