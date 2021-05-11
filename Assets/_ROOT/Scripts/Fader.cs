using System;
using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private RectTransform mask;

    [SerializeField]
    private float fadeInTime;

    [SerializeField]
    private float fadeOutTime;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerable ScaleCoroutine(object[] parms)
    {
        var time = (float) parms[0];
        Vector2 targetScale = (Vector2) parms[1];
        float elapsedTime = 0;
 
        while (elapsedTime < time)
        {
            mask.localScale = Vector3.Lerp(mask.transform.localScale, targetScale, (elapsedTime / time));
 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    public void FadeIn()
    {
        object[] parms = new object[2]{fadeInTime, Vector2.zero};
        StartCoroutine("ScaleCoroutine", parms);
    }

    public void FadeOut()
    {
        object[] parms = new object[2]{fadeOutTime, Vector2.one * 42000};
        StartCoroutine("ScaleCoroutine", parms);
    }
}