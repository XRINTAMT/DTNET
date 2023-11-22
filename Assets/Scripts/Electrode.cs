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
    private Pad LastPad;

    public void Connect()
    {
        Invoke("ProcessConnection", Time.fixedDeltaTime);
    }

    public void ProcessConnection()
    {
        Pad _p = GetComponentInChildren<Pad>();
        LastPad = _p;
        ID = _p.ID;
        _p.GetComponent<Sensor>().Connect();
        GetComponent<Grabbable>().enabled = false;
    }

    public void Disconnect()
    {
        ID = -1;
        if(LastPad != null)
        {
            LastPad.GetComponent<Sensor>().Disconnect();
            GetComponent<Grabbable>().enabled = true;
        }
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
