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
        }
    }

    void Connect(int n) 
    {
        GetComponent<PhotonView>().RPC("ConnectRPC", RpcTarget.OthersBuffered, n);
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
}
