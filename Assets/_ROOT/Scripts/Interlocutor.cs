﻿using UnityEngine;

public class Interlocutor : Touchable
{
    public override void OnTouch()
    {
        base.OnTouch();
        Debug.Log("I speak with you");
    }
}
