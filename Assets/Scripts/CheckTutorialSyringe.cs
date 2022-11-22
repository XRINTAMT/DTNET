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
    public int value;
    public UnityEvent valueReached = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Int32.Parse(textValueML.text)==value) valueReached.Invoke();
    }
    public void SetValue(int val) 
    {
        value = val;
    }

}
