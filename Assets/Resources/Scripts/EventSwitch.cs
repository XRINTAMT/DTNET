using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSwitch : MonoBehaviour
{
    [SerializeField] bool state;
    [SerializeField] UnityEvent OnTrue, OnFalse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Fire()
    {
        if (state)
            OnTrue.Invoke();
        else
            OnFalse.Invoke();
    }
    
    public void Switch()
    {
        state = !state;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
