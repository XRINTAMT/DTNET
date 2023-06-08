using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnJointBreak : MonoBehaviour
{
    [SerializeField] UnityEvent ToDo;

    private void OnJointBreak(float breakForce)
    {
        ToDo.Invoke();
    }
}
