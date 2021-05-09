using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : DesiredVelocityProvider
{
    [SerializeField, Range(0.5f, 5f)]
    private float circleDistance = 1;

    [SerializeField, Range(0.5f, 5f)]
    private float circleRadius = 2;

    [SerializeField, Range(1, 80)]
    private int angleChangeStep = 15;

    private int angle = 0;
    public override Vector3 GetDesiredVelocity()
    {
        var rnd = Random.value;
        if (rnd < 0.5)
        {
            angle += angleChangeStep;
        }
        else if(rnd < 1)
        {
            angle -= angleChangeStep;
        }
        // Debug.Log(GetComponent<Animal>().Velocity);
        var futurePosition = Interlocutor.transform.position + Interlocutor.Velocity.normalized * circleDistance;
        var vector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
        return (futurePosition + vector - transform.position).normalized * Interlocutor.VelocityLimit;
    }
}