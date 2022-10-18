using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] VitalsMonitor Monitor;
    [field: SerializeField] public string[] ValuesScanned { private set; get; }
    [SerializeField] int[] ports;
    public bool AffectsPatient = false;
    [field: SerializeField] public PatientData patient { private set; get; }

    void Start()
    {
        
    }

    public void SendData(string name, int value)
    {
        Monitor.ChangeFromSensor(name, value);
    }

    public void Connect()
    {
        for(int i = 0; i < ports.Length; i++)
        {
            Monitor.Connect(ports[i]);
            Debug.Log("Connecting port " + ports[i]);
            if (AffectsPatient)
            {
                patient = GetComponentInParent<PatientData>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
