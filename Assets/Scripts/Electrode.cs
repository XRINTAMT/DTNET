using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrode : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }


    public void Connect()
    {
        Invoke("ProcessConnection", Time.fixedDeltaTime);
    }

    public void ProcessConnection()
    {
        Pad _p = GetComponentInChildren<Pad>();
        ID = _p.ID;
    }

    public void Disconnect()
    {
        ID = -1;
    }
}
