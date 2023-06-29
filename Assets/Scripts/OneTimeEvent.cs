using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneTimeEvent : MonoBehaviour
{
    [SerializeField] UnityEvent ToDo;
    [SerializeField] bool Done;

    public void Fire()
    {
        if (!Done)
        {
            Done = true;
            ToDo.Invoke();
        }
    }
}
