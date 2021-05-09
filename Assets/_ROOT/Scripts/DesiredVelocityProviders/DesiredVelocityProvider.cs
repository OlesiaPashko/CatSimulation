using UnityEngine;

public abstract class DesiredVelocityProvider:MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private float weight = 1;

    public float Weight { get { return weight; } set { weight = value; } }

    protected Interlocutor Interlocutor;

    private void Awake()
    {
        Interlocutor = GetComponent<Interlocutor>();
    }
    public abstract Vector3 GetDesiredVelocity();
}