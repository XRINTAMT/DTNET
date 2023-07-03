using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class VitalsMonitorPhoton : MonoBehaviour
{
    [SerializeField] VitalsMonitor vitalsMonitor;
    [SerializeField] GameObject placePressure;
    [SerializeField] GameObject sylinderPressure;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        sylinderPressure = GameObject.Find("CylinderPrssure");
        vitalsMonitor = GameObject.Find("VitalsMonitor").GetComponent<VitalsMonitor>();
        PlacePoint[] placePoints = FindObjectsOfType<PlacePoint>(true);
        for (int i = 0; i < placePoints.Length; i++)
        {
            if (placePoints[i].name == "PlacePressure")
            {
                placePressure = placePoints[i].transform.GetChild(1).gameObject;
            }
        }

        if (!PhotonManager._viewerApp)
        {
            vitalsMonitor.conneñt += Connect;
            vitalsMonitor.alarm += Alarm;
        }
    }

    void Connect(int n) 
    {
        if (!PhotonManager._viewerApp) 
        {
            GetComponent<PhotonView>().RPC("ConnectRPC", RpcTarget.AllBuffered, n);
        }

    }
          

    void Alarm(bool alarm) 
    {
        if (!PhotonManager._viewerApp) 
        {
            GetComponent<PhotonView>().RPC("AlarmRPC", RpcTarget.All, alarm);
        }
            
    }
    [PunRPC]
    void ConnectRPC(int n)
    {
        Debug.Log("ConnectPad_RPC");
        if (PhotonManager._viewerApp)
        {
            if (n==2)
            {
                placePressure.SetActive(true);
                sylinderPressure.SetActive(false);
            }

            vitalsMonitor.Connect(n);
        }
    }
    [PunRPC]
    void AlarmRPC(bool alarm)
    {
        Debug.Log("Alarm_RPC");
        if (PhotonManager._viewerApp)
        {
            vitalsMonitor.SwitchAlarm(alarm);
        }
    }
}
