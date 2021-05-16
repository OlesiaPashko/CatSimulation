using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 10f;

    private List<GameAction> gameActions = new List<GameAction>();

    private Coroutine currentCoroutine = null;

    public void CalculateBest(float time)
    {
        //var timeToDoAction = DateTime.UtcNow.AddSeconds(time);
        gameActions = new List<GameAction>();
        var position = transform.position;
        var startNeedsFulfill = FindObjectOfType<NeedsFulfill>();
        var fulfill = startNeedsFulfill.EmulateTimeForNeedsFulfill(startNeedsFulfill.CurrentFulfill, time);
        // while (DateTime.UtcNow < timeToDoAction)
        //{
        for (int i = 0; i < 30; i++)
        {
            var action = GetBestAction(position, fulfill);
            gameActions.Add(action);
            position = action.FinalPoint;
            fulfill = action.FinalNeedsFulfill;
            Debug.Log($"action.FinalNeedsFulfill");
            Show(action.FinalNeedsFulfill);
        }
    }

    private void Show(Dictionary<InteractableType, float> needsFulfill)
    {
        foreach (var needFulfill in needsFulfill)
        {
            Debug.Log($"{needFulfill.Key} = {needFulfill.Value}");
        }
    }

    public void StartDoingBest()
    {
        currentCoroutine = StartCoroutine(DoBest());
    }
    
    public void StopDoingBest()
    {
        Debug.Log("Stop doing best");
        if(currentCoroutine != null)
        StopCoroutine(currentCoroutine);
    }

    private IEnumerator DoBest()
    {
        foreach (var action in gameActions)
        {
            action.Interactable.Prepare();
            GetComponent<AutoMove>().GoToAndInteract(action.StartPoint, action.Interactable);
            yield return new WaitForSeconds(action.Time);
        }
    }

    private GameAction GetBestAction(Vector3 position, Dictionary<InteractableType, float> startFulfill)
    {
        var bestInteractable = GetBestInteractable(position, startFulfill);
        var gameAction = EmulateGoToAndInteract(position, bestInteractable);
        return gameAction;
    }

    private GameAction EmulateGoToAndInteract(Vector3 playerPosition,
        (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) bestOption)
    {
        var bestInteractable = bestOption.Interactable;
        var direction = GetComponent<AutoMove>().GetDirection(playerPosition, bestInteractable);
        var finalPosition = direction + playerPosition;
        var speed = GetComponent<AutoMove>().Speed;
        var time = direction.magnitude / speed;
        var fullTime = time + bestInteractable.InteractionTime;
        var newFulfill = FindObjectOfType<NeedsFulfill>().EmulateTimeForNeedsFulfill(bestOption.NeedsFulfill, fullTime);
        return new GameAction()
        {
            StartPoint = playerPosition,
            FinalPoint = finalPosition,
            Interactable = bestInteractable,
            Time = fullTime,
            FinalNeedsFulfill = newFulfill
        };
    }
    

    private (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) GetBestInteractable(
        Vector3 position, Dictionary<InteractableType, float> needsFulfill)
    {
        var bestVariant = GetBestInteractableAtRadius(position, needsFulfill);

        if (bestVariant.Interactable == null)
        {
            return GetBestInteractableAtAllMap(position, needsFulfill);
        }

        return bestVariant;
    }

    private (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) GetBestInteractableAtRadius(
        Vector3 position, Dictionary<InteractableType, float> needsFulfill)
    {
        var colliders = Physics.OverlapSphere(position, radius);
        var bestIncrease = 0f;
        Interactable bestInteractable = null;
        Dictionary<InteractableType, float> futureNeedsFulfill = null;
        Dictionary<InteractableType, float> distances = new Dictionary<InteractableType, float>()
        {
            {InteractableType.Communication, float.MaxValue},
            {InteractableType.Food, float.MaxValue},
            {InteractableType.Toilet, float.MaxValue}
        };
        foreach (var collider in colliders)
        {
            var interactable = collider.gameObject.GetComponent<Interactable>();

            if (interactable != null)
            {
                var currentFulfill = needsFulfill[interactable.Type];
                var revenue = interactable.Revenue;
                var futureNeedFulfill = revenue + currentFulfill > 100
                    ? 100
                    : revenue + currentFulfill;
                var increase = GetIncrease(interactable.Type, futureNeedFulfill, currentFulfill);
                var distance = (position - interactable.transform.position).magnitude;
                if (increase > 0 && increase >= bestIncrease)
                {
                    if (Math.Abs(increase - bestIncrease) < 0.1f && distances[interactable.Type] < distance)
                    {
                        continue;
                    }

                    bestIncrease = increase;
                    bestInteractable = interactable;
                    futureNeedsFulfill = new Dictionary<InteractableType, float>(needsFulfill);
                    futureNeedsFulfill[interactable.Type] = futureNeedFulfill;
                }
            }
        }

        (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) bestVariant =
            (bestInteractable, futureNeedsFulfill);
        return bestVariant;
    }

    private float GetIncrease(InteractableType type, float futureNeedFulfill, float currentFulfill)
    {
        var increase = futureNeedFulfill - currentFulfill;
        var feature = CharacterSettings.GetFeatureForNeed(type);
        increase *= CharacterSettings.Features[feature];
        return increase;
    }

    private (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) GetBestInteractableAtAllMap(
        Vector3 position, Dictionary<InteractableType, float> needsFulfill)
    {
        float bestIncrease = 0;
        InteractableType bestType = InteractableType.Food;
        Dictionary<InteractableType, float> futureNeedsFulfill = null;
        foreach (InteractableType type in (InteractableType[]) Enum.GetValues(typeof(InteractableType)))
        {
            var currentFulfill = needsFulfill[type];
            var revenue = InteractableSettings.Revenues[type];
            var futureNeedFulfill = revenue + currentFulfill > 100
                ? 100
                : revenue + currentFulfill;
            var increase = GetIncrease(type, futureNeedFulfill, currentFulfill);
            if (increase > bestIncrease)
            {
                bestIncrease = increase;
                bestType = type;
                futureNeedsFulfill = new Dictionary<InteractableType, float>(needsFulfill);
                futureNeedsFulfill[type] = futureNeedFulfill;
            }
        }

        var closestInteractable = GetClosestInteractableOfType(bestType, position);

        (Interactable Interactable, Dictionary<InteractableType, float> NeedsFulfill) bestVariant =
            (closestInteractable, futureNeedsFulfill);
        return bestVariant;
    }

    private Interactable GetClosestInteractableOfType(InteractableType type, Vector3 position)
    {
        var interactables = FindObjectsOfType<Interactable>().Where(i => i.Type == type);
        var minDistance = float.MaxValue;
        var closestInteractable = interactables.First();
        foreach (var interactable in interactables)
        {
            var distance = (position - interactable.transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestInteractable = interactable;
            }
        }

        return closestInteractable;
    }
}