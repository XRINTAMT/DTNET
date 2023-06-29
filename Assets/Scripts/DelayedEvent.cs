using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    [SerializeField] int timeInSeconds;
    [SerializeField] UnityEvent Event;
    
    public void Fire()
    {
        Invoke("DelayedStart", timeInSeconds);
    }

    private void DelayedStart()
    {
        Event.Invoke();
    }
}
