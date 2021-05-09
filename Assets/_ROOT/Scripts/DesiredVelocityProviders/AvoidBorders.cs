using UnityEngine;

public class AvoidBorders : DesiredVelocityProvider
{
    private float edge = 0.2f;
    public override Vector3 GetDesiredVelocity()
    {
        var cam = Camera.main;
        var maxSpeed = Interlocutor.VelocityLimit;
        var v = Interlocutor.Velocity;
        if(cam == null)
        {
            //Debug.Log("0");
            return v;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 desired = Vector3.zero;
        if (pos.x < edge) 
        { 
            //Debug.Log("I am left of the camera's view.");
            desired += new Vector3(maxSpeed, 0, Interlocutor.Velocity.z);
        }
        if (1.0 - edge < pos.x)
        {
            //Debug.Log("I am right of the camera's view.");
            desired += new Vector3(-maxSpeed, 0, Interlocutor.Velocity.z);
        }
        if (pos.y < edge)
        {
            //Debug.Log("I am below the camera's view.");
            desired += new Vector3(Interlocutor.Velocity.x, 0, maxSpeed);
        }
        if (1.0 - edge < pos.y)
        {
            //Debug.Log("I am above the camera's view.");
            desired += new Vector3(Interlocutor.Velocity.x, 0, -maxSpeed);
        }
        return desired;
    }
}
