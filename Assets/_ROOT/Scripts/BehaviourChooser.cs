using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BehaviourChooser : MonoBehaviour
{
    [SerializeField] private VariableJoystick variableJoystick;

    [SerializeField] private float timer;
    [SerializeField] private float maxTime;
    [SerializeField] private bool isTimerEnabled;
    void Start()
    {
        variableJoystick.PointerIsUp += OnPointerUp;
        variableJoystick.PointerIsDown += OnPointerDown;
        isTimerEnabled = true;
        var autoBehaviour = gameObject.GetComponent<AutoBehaviour>();
        autoBehaviour.CalculateBest(maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerEnabled)
        {
            var seconds = (int)timer % 60;
            if (seconds < maxTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Debug.Log("That`s time!!");
                if (isTimerEnabled)
                {
                    var autoBehaviour = gameObject.GetComponent<AutoBehaviour>();
                    autoBehaviour.StartDoingBest();
                    isTimerEnabled = false;
                }
            }
        }
    }

    private void OnPointerUp()
    {
        isTimerEnabled = true;
        var autoBehaviour = gameObject.GetComponent<AutoBehaviour>();
        autoBehaviour.CalculateBest(maxTime);
    }
    private void OnPointerDown()
    {
        var autoBehaviour = gameObject.GetComponent<AutoBehaviour>();
        autoBehaviour.StopDoingBest();
        isTimerEnabled = false;
        timer = 0;
    }
}
