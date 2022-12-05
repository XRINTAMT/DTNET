using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckTutorialSyringe : MonoBehaviour
{
    [SerializeField]  Syringe syringe;
    [SerializeField] TutorialEditor tutorialEditor;
    public int value;
    public UnityEvent valueReached = new UnityEvent();
    int countCompleteTask;
    public float valueSyringe;
    bool dial=true;
    
    // Start is called before the first frame update
    void Start()
    {
        syringe = GetComponent<Syringe>();
    }

    // Update is called once per frame
    void Update()
    {
        valueSyringe = syringe.totalSubstance;
 
        if (dial)
        {
            if (valueSyringe >= value)
            {
                Debug.Log(valueSyringe);
                tutorialEditor.CompleteTask(countCompleteTask);
                valueReached.Invoke();
                dial = false;
            }
        }
        if (!dial)
        {
            if (valueSyringe <= value)
            {
                tutorialEditor.CompleteTask(countCompleteTask);
                valueReached.Invoke();
                dial = true;
            }
        }
        
    }
    public void SetValue(int val) 
    {
        value = val;
    }
    public void SetCountCompleteTask(int countTask)
    {
        countCompleteTask = countTask;
    }
}
