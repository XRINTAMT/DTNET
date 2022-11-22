using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckTutorialSyringe : MonoBehaviour
{
    [SerializeField]  Text textValueML;
    [SerializeField] TutorialEditor tutorialEditor;
    public int value;
    public UnityEvent valueReached = new UnityEvent();
    int countCompleteTask;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (int.Parse(textValueML.text) == value)
        {
            tutorialEditor.CompleteTask(countCompleteTask);
            valueReached.Invoke();
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
