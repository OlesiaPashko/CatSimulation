using UnityEngine;

public class Toilet : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        Debug.Log("пісь пісь пісь");
    }
}
