using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] VitalsMonitor Monitor;
    [field: SerializeField] public string[] ValuesScanned { private set; get; }
    [SerializeField] int[] ports;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
