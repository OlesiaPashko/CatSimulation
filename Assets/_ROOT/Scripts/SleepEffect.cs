using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SleepEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 scaleChange = new Vector3(-0.001f, -0.001f, -0.001f);

    [SerializeField]
    private Vector2 positionChange = new Vector2(-10000f, -10000f);

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange;
        (transform as RectTransform).anchoredPosition += positionChange;
        Debug.Log($"<color=red>(transform as RectTransform).anchoredPosition = {(transform as RectTransform).anchoredPosition}</color>");

        if (transform.localScale.y < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
