using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FunEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

    [SerializeField]
    private float positionMaxChange = 2f;
    private Vector2 positionChange;

    private void Start()
    {
        positionChange = new Vector2(Random.Range(-positionMaxChange, positionMaxChange),
            Random.Range(-positionMaxChange, positionMaxChange));
    }

    void Update()
    {
        transform.localScale += scaleChange;
        (transform as RectTransform).anchoredPosition += positionChange;

        if (transform.localScale.y < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
