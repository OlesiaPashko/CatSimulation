using UnityEngine;

public class AvoidBorders : DesiredVelocityProvider
{
    private float minX = -25f;
    private float maxX = 10f;
    private float minZ = -40f;
    private float maxZ = 0f;
    private float edge = 5f;
    public override Vector3 GetDesiredVelocity()
    {
        var maxSpeed = Interlocutor.VelocityLimit;
        Vector3 pos = transform.position;
        Vector3 desired = Vector3.zero;
        if (pos.x < minX + edge) 
        { 
            Debug.Log("I am left of the camera's view.");
            desired += new Vector3(maxSpeed,0,  Interlocutor.Velocity.z);
        }
        if (maxX - edge < pos.x)
        {
            Debug.Log("I am right of the camera's view.");
            desired += new Vector3(-maxSpeed, 0, Interlocutor.Velocity.z);
        }
        if (pos.z < minZ + edge)
        {
            Debug.Log("I am below the camera's view.");
            desired += new Vector3(Interlocutor.Velocity.x, 0, maxSpeed);
        }
        if (maxZ - edge > pos.z)
        {
            Debug.Log("I am above the camera's view.");
            desired += new Vector3(Interlocutor.Velocity.x, 0, -maxSpeed);
        }
        return desired;
    }
}