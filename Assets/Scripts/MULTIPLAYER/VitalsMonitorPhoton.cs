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
            VitalsMonitor vitalsMonitor = FindObjectOfType<VitalsMonitor>();
            vitalsMonitor.conneñt += Connect;
            vitalsMonitor.alarm += Alarm;
        }
    }

    void Connect(int n) 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("ConnectRPC", RpcTarget.AllBuffered, n);
    }

    void Alarm(bool alarm) 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AlarmRPC", RpcTarget.All, alarm);
    }
    [PunRPC]
    void ConnectRPC(int n)
    {
        Debug.Log("ConnectPad_RPC");
        if (PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = FindObjectOfType<VitalsMonitor>();
            vitalsMonitor.Connect(n);
        }
    }
    [PunRPC]
    void AlarmRPC(bool alarm)
    {
        Debug.Log("Alarm_RPC");
        if (PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = FindObjectOfType<VitalsMonitor>();
            vitalsMonitor.SwitchAlarm(alarm);
        }
    }
}
