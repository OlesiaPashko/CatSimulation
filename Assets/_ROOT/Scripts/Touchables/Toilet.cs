using UnityEngine;

public class Toilet : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        FindObjectOfType<ToiletCounter>().Count += 10;
        Debug.Log("пісь пісь пісь");
    }
}
