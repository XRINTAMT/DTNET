using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class Electrode : DataSaver
{
    [field: SerializeField] public int ID { get; private set; }
    private int savedID;
    private bool GrabbableActive;
    private bool PlacePointActive;

    public void Connect()
    {
        Invoke("ProcessConnection", Time.fixedDeltaTime);
    }

    public void ProcessConnection()
    {
        Pad _p = GetComponentInChildren<Pad>();
        ID = _p.ID;
        _p.GetComponent<Sensor>().Connect();
    }

    public void Disconnect()
    {
        ID = -1;
    }

    public override void Save()
    {
        savedID = ID;
        GrabbableActive = GetComponent<Grabbable>().enabled;
        PlacePointActive = GetComponent<PlacePoint>().enabled;
    }

    public override void Load()
    {
        ID = savedID;
        GetComponent<Grabbable>().enabled = GrabbableActive;
        GetComponent<PlacePoint>().enabled = PlacePointActive;
    }
}
