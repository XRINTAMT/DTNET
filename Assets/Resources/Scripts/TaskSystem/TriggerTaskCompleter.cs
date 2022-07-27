using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTaskCompleter : MonoBehaviour
{
    [SerializeField] GameObject[] TriggerObjects;
    [SerializeField] UnityEvent TriggerEnter;
    [SerializeField] UnityEvent TriggerLeave;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (TriggerObjects.Length != 0)
        {
            for (int i = 0; i < TriggerObjects.Length; i++)
            {
                if (other.gameObject == TriggerObjects[i])
                {
                    TriggerEnter.Invoke();
                }
            }
            
        }
        else
        {
            TriggerEnter.Invoke();
        }
    }

    private void OnTriggerLeave(Collider other)
    {
        if (TriggerObjects.Length != 0)
        {
            for (int i = 0; i < TriggerObjects.Length; i++)
            {
                if (other.gameObject == TriggerObjects[i])
                {
                    TriggerLeave.Invoke();
                }
            }

        }
        else
        {
            TriggerEnter.Invoke();
        }
    }
}
