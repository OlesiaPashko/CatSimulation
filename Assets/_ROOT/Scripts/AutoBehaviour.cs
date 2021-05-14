using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField]
    private float radius = 10f;

    [SerializeField]
    private float speed = 2f;

    private List<GameAction> gameActions = new List<GameAction>();
    public void CalculateBest(float time)
    {
        //var timeToDoAction = DateTime.UtcNow.AddSeconds(time);
        gameActions = new List<GameAction>();
        var position = transform.position;
       // while (DateTime.UtcNow < timeToDoAction)
        //{
        for(int i = 0;i<4;i++)
        {
            var action = GetBestAction(position);
            gameActions.Add(action);
            position = action.FinalPoint;
        }
    }

    public void StartDoingBest()
    {
        StartCoroutine(DoBest(gameActions));
    }

    private IEnumerator DoBest(List<GameAction> actions)
    {
        foreach (var action in actions)
        {
            action.Interactable.Prepare();
            GoTo(action.StartPoint, action.Interactable);
            yield return new WaitForSeconds(action.Time);
        }
    }

    private GameAction GetBestAction(Vector3 position)
    {
        var bestInteractable = GetBestInteractable(position);
        var gameAction = EmulateGoToAndInteract(position, bestInteractable);
        return gameAction;
    }

    private float GoTo(Vector3 playerPosition, Interactable bestAction)
    {
        var direction = GetDirection(playerPosition, bestAction);
        transform.rotation = Quaternion.LookRotation(direction);
        var finalPosition = direction + transform.position;
        var time = direction.magnitude / speed;
        StartCoroutine(SmoothLerp(time, finalPosition, bestAction.Interact));
        return time;
    }

    private GameAction EmulateGoToAndInteract(Vector3 playerPosition, Interactable bestInteractable)
    {
        var direction = GetDirection(playerPosition, bestInteractable);
        var finalPosition = direction + playerPosition;
        var time = direction.magnitude / speed;
        return new GameAction()
        {
            StartPoint = playerPosition,
            FinalPoint = finalPosition,
            Interactable = bestInteractable,
            Time = time + bestInteractable.InteractionTime
        };
    }

    private Vector3 GetDirection(Vector3 playerPosition, Interactable bestAction)
    {
        var bestPosition = bestAction.transform.position;
        var bestActionPosition = new Vector3(bestPosition.x, playerPosition.y, bestPosition.z);
        var direction = bestActionPosition - playerPosition;
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

    private Interactable GetBestInteractable(Vector3 position)
    {
        var colliders = Physics.OverlapSphere(position, radius);
        var bestIncrease = 0f;
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

                var increase = (float) futureNeedFulfill - currentFulfill;
                Debug.Log($"increase = {increase}");
                var feature = CharacterSettings.GetFeatureForNeed(interactable.Type);
                increase *= CharacterSettings.Features[feature];
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