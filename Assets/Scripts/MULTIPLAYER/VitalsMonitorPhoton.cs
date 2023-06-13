using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class VitalsMonitorPhoton : MonoBehaviour
{

    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = GetComponent<VitalsMonitor>();
            vitalsMonitor.conneñt += Connect;
            vitalsMonitor.alarm += Alarm;
        }
    }

    void Connect(int n) 
    {
        GetComponent<PhotonView>().RPC("ConnectRPC", RpcTarget.OthersBuffered, n);
    }

    void Alarm(bool alarm) 
    {
        Debug.Log("Alarm");
        //GetComponent<PhotonView>().RPC("AlarmRPC", RpcTarget.Others, alarm);
    }
    [PunRPC]
    void ConnectRPC(int n)
    {
        if (PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = GetComponent<VitalsMonitor>();
            vitalsMonitor.Connect(n);
        }
    }
    [PunRPC]
    void AlarmRPC(bool alarm)
    {
        if (PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = GetComponent<VitalsMonitor>();
            vitalsMonitor.SwitchAlarm(alarm);
        }
    }
}
