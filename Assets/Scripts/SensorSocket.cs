using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSocket : MonoBehaviour
{
    [SerializeField] PatientData patient;

    void Start()
    {
        
    }

    public void AttachSensor()
    {
        StartCoroutine(WaitForSensor());
    }

    IEnumerator WaitForSensor()
    {
        yield return 0;
        Sensor toAttach = GetComponentInChildren<Sensor>();
        if (toAttach == null)
        {
            Debug.LogError("No sensor found!");
        }
        else
        {
            patient.Subscribe(toAttach);
        }
        
    }
    
    void Update()
    {
        
    }
}
