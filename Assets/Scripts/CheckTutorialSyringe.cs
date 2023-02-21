using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckTutorialSyringe : MonoBehaviour
{
    [SerializeField] GameObject panelUICome30True;
    [SerializeField] GameObject panelUICome30False;
    [SerializeField] GameObject placeSyringe;
    [SerializeField]  Syringe syringe;
    [SerializeField] TutorialEditor tutorialEditor;
    public int value;
    public UnityEvent valueReached = new UnityEvent();
    public UnityEvent valueReachedBack = new UnityEvent();
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
            if (valueSyringe <= value && valueSyringe >= value - 1)
            {
                if (!panelUICome30True.activeSelf)
                {
                    tutorialEditor.CompleteTask(countCompleteTask);
                    placeSyringe.SetActive(true);
                    panelUICome30True.SetActive(true);
                    panelUICome30False.SetActive(false);
                }
     
                //Invoke()
                //valueReached.Invoke();
                //dial = true;
            }
            if (valueSyringe <= value-1)
            {
                if (!panelUICome30False.activeSelf)
                {
                    placeSyringe.SetActive(false);
                    panelUICome30True.SetActive(false);
                    panelUICome30False.SetActive(true);
                }
                //tutorialEditor.CompleteTask(countCompleteTask);
                //valueReachedBack.Invoke();
                //dial = true;
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
