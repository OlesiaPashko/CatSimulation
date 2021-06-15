using System;
using System.Collections;
using UnityEngine;

public class Interlocutor : Interactable
{
    
    [SerializeField]
    private float interactionTime;
    
    [SerializeField] private Animator animator;

    
    public override float InteractionTime => interactionTime;
    public override InteractableType Type { get; set; }

    private void Awake()
    {
        Type = InteractableType.Communication;
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine("ExecuteAfterTime");
    }
    
    IEnumerator ExecuteAfterTime()
    {
        animator.SetTrigger("StartTalking");

        yield return new WaitForSeconds(InteractionTime);

        isWandering = true;
        animator.SetTrigger("StartWalking");

    }

    public override void Prepare(Vector3 direction)
    {
        base.Prepare(direction);
        animator.SetTrigger("StopWalking");
        isWandering = false;
        transform.rotation = Quaternion.LookRotation(direction * -1f);
    }

    protected Vector3 velocity;
    protected Vector3 acceleration;

    [SerializeField, Range(1, 20)]
    private float radius = 10f;

    private bool isWandering = true;

    void Update()
    {
        if (isWandering)
        {
            ApplyForcesFromProviders();
        }
    }
    
    public Vector3 Velocity { 
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
        }
    }

    [SerializeField]
    private float mass = 1;

    [SerializeField, Range(1, 20)]
    private static float velocityLimit = 3;

    [SerializeField, Range(1, 20)]
    private float steeringForceLimit = 3;

    private const float Epsilon = 0.01f;
    public float VelocityLimit => velocityLimit;

    public void ApplyForce(Vector3 force)
    {
        force /= mass;
        acceleration += force;
    }

    protected void ApplyForcesFromProviders()
    {
        ApplyFriction();

        ApplySteeringForce();

        ApplyForces();

        void ApplyFriction()
        {
            var friction = -velocity.normalized * 0.1f;
            ApplyForce(friction);
        }

        void ApplySteeringForce()
        {
            var providers = GetComponents<DesiredVelocityProvider>();
            if (providers.Length == 0)
            {
                // Debug.LogError("Here");
                return;
            }
            Vector3 desiredVelocity = Vector3.zero;
            foreach (var provider in providers)
            {
                var velocity = provider.GetDesiredVelocity();
                desiredVelocity += velocity.normalized * provider.Weight;
            }
            var steeringForce = desiredVelocity - velocity;

            ApplyForce(steeringForce.normalized * steeringForceLimit);
        }

        void ApplyForces()
        {
            velocity += acceleration * Time.deltaTime;

            velocity = Vector3.ClampMagnitude(velocity, velocityLimit);

            if(velocity.magnitude < Epsilon)
            {
                velocity = Vector3.zero;
                return;
            }

            transform.position += velocity * Time.deltaTime;
            acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
    
    
}
