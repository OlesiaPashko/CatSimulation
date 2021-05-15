using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 10f;

    [SerializeField] private float speed = 2f;

    private List<GameAction> gameActions = new List<GameAction>();

    public void CalculateBest(float time)
    {
        //var timeToDoAction = DateTime.UtcNow.AddSeconds(time);
        gameActions = new List<GameAction>();
        var position = transform.position;
        var needsFulfill = FindObjectOfType<NeedsFulfill>().CurrentFulfill;
        // while (DateTime.UtcNow < timeToDoAction)
        //{
        for (int i = 0; i < 4; i++)
        {
            var action = GetBestAction(position, needsFulfill);
            gameActions.Add(action);
            position = action.FinalPoint;
            needsFulfill = action.FinalNeedsFulfill;
            Debug.Log($"action.FinalNeedsFulfill");
            Show(action.FinalNeedsFulfill);
        }
    }

    private void Show(Dictionary<InteractableType, int> needsFulfill)
    {
        foreach (var needFulfill in needsFulfill)
        {
            Debug.Log($"{needFulfill.Key} = {needFulfill.Value}");
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

    private GameAction GetBestAction(Vector3 position, Dictionary<InteractableType, int> startFulfill)
    {
        var bestInteractable = GetBestInteractable(position, startFulfill);
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

    private GameAction EmulateGoToAndInteract(Vector3 playerPosition,
        (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) bestOption)
    {
        var bestInteractable = bestOption.Interactable;
        var direction = GetDirection(playerPosition, bestInteractable);
        var finalPosition = direction + playerPosition;
        var time = direction.magnitude / speed;
        return new GameAction()
        {
            StartPoint = playerPosition,
            FinalPoint = finalPosition,
            Interactable = bestInteractable,
            Time = time + bestInteractable.InteractionTime,
            FinalNeedsFulfill = bestOption.NeedsFulfill
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

    private (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) GetBestInteractable(
        Vector3 position, Dictionary<InteractableType, int> needsFulfill)
    {
        var bestVariant = GetBestInteractableAtRadius(position, needsFulfill);

        if (bestVariant.Interactable == null)
        {
            return GetBestInteractableAtAllMap(position, needsFulfill);
        }

        return bestVariant;
    }

    private (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) GetBestInteractableAtRadius(
        Vector3 position, Dictionary<InteractableType, int> needsFulfill)
    {
        var colliders = Physics.OverlapSphere(position, radius);
        var bestIncrease = 0f;
        Interactable bestInteractable = null;
        Dictionary<InteractableType, int> futureNeedsFulfill = null;
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


                var increase = (float) futureNeedFulfill - currentFulfill;
                var feature = CharacterSettings.GetFeatureForNeed(interactable.Type);
                increase *= CharacterSettings.Features[feature];

                var distance = (position - interactable.transform.position).magnitude;
                if (increase > 0 && increase >= bestIncrease)
                {
                    if (Math.Abs(increase - bestIncrease) < 0.1f && distances[interactable.Type] < distance)
                    {
                        continue;
                    }

                    bestIncrease = increase;
                    bestInteractable = interactable;
                    futureNeedsFulfill = new Dictionary<InteractableType, int>(needsFulfill);
                    futureNeedsFulfill[interactable.Type] = futureNeedFulfill;
                }
            }
        }

        (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) bestVariant =
            (bestInteractable, futureNeedsFulfill);
        return bestVariant;
    }

    private (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) GetBestInteractableAtAllMap(
        Vector3 position, Dictionary<InteractableType, int> needsFulfill)
    {
        float bestIncrease = 0;
        InteractableType bestType = InteractableType.Food;
        Dictionary<InteractableType, int> futureNeedsFulfill = null;
        foreach (InteractableType type in (InteractableType[]) Enum.GetValues(typeof(InteractableType)))
        {
            var currentFulfill = needsFulfill[type];
            var revenue = InteractableSettings.Revenues[type];
            var futureNeedFulfill = revenue + currentFulfill > 100
                ? 100
                : revenue + currentFulfill;


            var increase = (float) futureNeedFulfill - currentFulfill;
            var feature = CharacterSettings.GetFeatureForNeed(type);
            increase *= CharacterSettings.Features[feature];
            if (increase > bestIncrease)
            {
                bestIncrease = increase;
                bestType = type;
                futureNeedsFulfill = new Dictionary<InteractableType, int>(needsFulfill);
                futureNeedsFulfill[type] = futureNeedFulfill;
            }
        }

        var closestInteractable = GetClosestInteractableOfType(bestType, position);

        (Interactable Interactable, Dictionary<InteractableType, int> NeedsFulfill) bestVariant =
            (closestInteractable, futureNeedsFulfill);
        return bestVariant;
    }

    private Interactable GetClosestInteractableOfType(InteractableType type, Vector3 position)
    {
        Debug.Log("INSIDE GetClosestInteractableOfType");
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