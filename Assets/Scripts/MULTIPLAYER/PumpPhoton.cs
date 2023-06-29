using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PumpPhoton : MonoBehaviour
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
            PerfusionPumpSettings perfusionPumpSettings = FindObjectOfType<PerfusionPumpSettings>();
            perfusionPumpSettings.rightButton+=Right;
            perfusionPumpSettings.leftButton += Left;
            perfusionPumpSettings.downButton += Down;
            perfusionPumpSettings.upButton += Up;
        }
    }

    void Right() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("RightRPC", RpcTarget.All);
    }
    void Left()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("LeftRPC", RpcTarget.All);
    }
    void Down()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("DownRPC", RpcTarget.All);
    }
    void Up()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("UpRPC", RpcTarget.All);
    }

    [PunRPC]
    void RightRPC()
    {
        Debug.Log("Right_RPC");
        if (PhotonManager._viewerApp)
        {
            PerfusionPumpSettings perfusionPumpSettings = FindObjectOfType<PerfusionPumpSettings>();
            perfusionPumpSettings.Right();
        }
    }

    [PunRPC]
    void LeftRPC()
    {
        Debug.Log("Left_RPC");
        if (PhotonManager._viewerApp)
        {
            PerfusionPumpSettings perfusionPumpSettings = FindObjectOfType<PerfusionPumpSettings>();
            perfusionPumpSettings.Left();
        }
    }
    [PunRPC]
    void DownRPC()
    {
        Debug.Log("Down_RPC");
        if (PhotonManager._viewerApp)
        {
            PerfusionPumpSettings perfusionPumpSettings = FindObjectOfType<PerfusionPumpSettings>();
            perfusionPumpSettings.Down();
        }
    }
    [PunRPC]
    void UpRPC()
    {
        Debug.Log("Up_RPC");
        if (PhotonManager._viewerApp)
        {
            PerfusionPumpSettings perfusionPumpSettings = FindObjectOfType<PerfusionPumpSettings>();
            perfusionPumpSettings.Up();
        }
    }

}
