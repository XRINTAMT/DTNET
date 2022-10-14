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
        Sensor toAttach = GetComponentInChildren<Sensor>();
        if (toAttach == null)
        {
            Debug.LogError("No sensor found!");
            return;
        }
        patient.Subscribe(toAttach);
    }
    
    void Update()
    {
        
    }
}
