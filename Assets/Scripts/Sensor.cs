using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : DataSaver
{
    [SerializeField] VitalsMonitor Monitor;
    [field: SerializeField] public string[] ValuesScanned { private set; get; }
    [SerializeField] int[] ports;
    [SerializeField] bool Connected = false;
    bool SavedConnected = false;

    public void SendData(string name, int value)
    {
        Monitor.ChangeFromSensor(name, value);
    }

    public void Connect()
    {
        if (!Connected)
        {
            Connected = true;
            for (int i = 0; i < ports.Length; i++)
            {
                Monitor.Connect(ports[i]);
                Debug.Log("Connecting port " + ports[i]);
            }
        }
    }

    public void Disconnect()
    {
        if (Connected)
        {
            Connected = false;
            for (int i = 0; i < ports.Length; i++)
            {
                Monitor.Disconnect(ports[i]);
                Debug.Log("Connecting port " + ports[i]);
            }
        }
    }

    public override void Save()
    {
        SavedConnected = Connected;
    }

    public override void Load()
    {
        Connected = SavedConnected;
    }
}
