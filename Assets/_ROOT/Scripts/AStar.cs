using System.Collections.Generic;

public static class AStar<T>
{
    private static readonly FastPriorityQueue<INode<T>, T> frontier;
    private static readonly Dictionary<T, INode<T>> stateToNode;
    private static readonly Dictionary<T, INode<T>> explored;

    private static readonly List<INode<T>> createdNodes;

    static AStar()
    {
        var maxNodesToExpand = 1000;
        frontier = new FastPriorityQueue<INode<T>, T>(maxNodesToExpand);
        stateToNode = new Dictionary<T, INode<T>>();
        explored = new Dictionary<T, INode<T>>(); // State -> node
        createdNodes = new List<INode<T>>(maxNodesToExpand);
    }

    static void ClearNodes()
    {
        foreach (var node in createdNodes)
        {
            node.Recycle();
        }

        createdNodes.Clear();
    }

    public static INode<T> Run(INode<T> start, T goal, int maxIterations = 100, bool earlyExit = true, bool clearNodes = true)
    {
        frontier.Clear();
        stateToNode.Clear();
        explored.Clear();
        if (clearNodes)
        {
            ClearNodes();
            createdNodes.Add(start);
        }

        frontier.Enqueue(start, start.GetCost());

        var iterations = 0;
        while ((frontier.Count > 0) && (iterations < maxIterations) && (frontier.Count + 1 < frontier.MaxSize))
        {
            var node = frontier.Dequeue();
            if (node.IsGoal(goal))
            {
                return node;
            }

            explored[node.GetState()] = node;


            foreach (var child in node.Expand())
            {
                iterations++;
                if (clearNodes)
                {
                    createdNodes.Add(child);
                }

                if (earlyExit && child.IsGoal(goal))
                {
                    return child;
                }

                var childCost = child.GetCost();
                var state = child.GetState();
                if (explored.ContainsKey(state))
                    continue;
                INode<T> similiarNode;
                stateToNode.TryGetValue(state, out similiarNode);
                if (similiarNode != null)
                {
                    if (similiarNode.GetCost() > childCost)
                        frontier.Remove(similiarNode);
                    else
                        break;
                }

                //Utilities.ReGoapLogger.Log(string.Format("    Enqueue frontier: {0}, cost: {1}", child.Name, childCost));
                frontier.Enqueue(child, childCost);
                stateToNode[state] = child;
            }
        }

        return null;
    }
}

public interface INode<T>
{
    T GetState();
    List<INode<T>> Expand();
    int CompareTo(INode<T> other);
    float GetCost();
    float GetHeuristicCost();
    float GetPathCost();
    INode<T> GetParent();
    bool IsGoal();

    string Name { get; }
    T Goal { get; }
    T Effects { get; }
    T Preconditions { get; }

    int QueueIndex { get; set; }
    float Priority { get; set; }
    void Recycle();
}

public class Node : INode<GameAction>
{
    private readonly GameAction gameAction;

    public Node(GameAction gameAction)
    {
        this.gameAction = gameAction;
    }
    public GameAction GetState()
    {
        return gameAction;
    }

    public List<INode<GameAction>> Expand()
    {
        throw new System.NotImplementedException();
    }

    public int CompareTo(INode<GameAction> other)
    {
        throw new System.NotImplementedException();
    }

    public float GetCost()
    {
        throw new System.NotImplementedException();
    }

    public float GetHeuristicCost()
    {
        throw new System.NotImplementedException();
    }

    public float GetPathCost()
    {
        throw new System.NotImplementedException();
    }

    public INode<GameAction> GetParent()
    {
        throw new System.NotImplementedException();
    }

    public bool IsGoal()
    {
        throw new System.NotImplementedException();
    }

    public string Name { get; }
    public GameAction Goal { get; }
    public GameAction Effects { get; }
    public GameAction Preconditions { get; }
    public int QueueIndex { get; set; }
    public float Priority { get; set; }
    public void Recycle()
    {
        throw new System.NotImplementedException();
    }
}

public class NodeComparer<T> : IComparer<INode<T>>
{
    public int Compare(INode<T> x, INode<T> y)
    {
        var result = x.CompareTo(y);
        if (result == 0)
            return 1;
        return result;
    }
}