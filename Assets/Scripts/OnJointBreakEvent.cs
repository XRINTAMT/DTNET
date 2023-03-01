using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnJointBreakEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onJointBreak;
    void OnJointBreak(float breakForce)
    {
        onJointBreak.Invoke();
    }
}
