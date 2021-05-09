using UnityEngine;

public class Interlocutor : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        FindObjectOfType<CommunicationCounter>().Count += 25;
        Debug.Log("I speak with you");
    }
}
