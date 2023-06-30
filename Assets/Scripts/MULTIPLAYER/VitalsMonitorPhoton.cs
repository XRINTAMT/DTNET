using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class VitalsMonitorPhoton : MonoBehaviour
{
    [SerializeField] VitalsMonitor vitalsMonitor;
    [SerializeField] GameObject placePressure;
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
            PlacePoint[] placePoints = FindObjectsOfType<PlacePoint>(true);
            for (int i = 0; i < placePoints.Length; i++)
            {
                if (placePoints[i].name== "PlacePressure")
                {
                    placePressure = placePoints[i].transform.GetChild(1).gameObject;
                }
            }

            vitalsMonitor = GameObject.Find("VitalsMonitor").GetComponent<VitalsMonitor>();
            
            vitalsMonitor.conne�t += Connect;
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
            }
            VitalsMonitor vitalsMonitor = GameObject.Find("VitalsMonitor").GetComponent<VitalsMonitor>(); ;
            vitalsMonitor.Connect(n);
        }
    }
    [PunRPC]
    void AlarmRPC(bool alarm)
    {
        Debug.Log("Alarm_RPC");
        if (PhotonManager._viewerApp)
        {
            VitalsMonitor vitalsMonitor = GameObject.Find("VitalsMonitor").GetComponent<VitalsMonitor>(); ;
            vitalsMonitor.SwitchAlarm(alarm);
        }
    }
}
