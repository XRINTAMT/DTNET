using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GuidePhoton : MonoBehaviourPunCallbacks
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
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            guideSystem.activateGuide += ActivateGuide;
        }
        if (PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            guideSystem.gameObject.SetActive(true);
        }
    }

    void ActivateGuide(int id)
    {
        //if (PhotonNetwork.CountOfPlayers == 1 && id == 1)
        //{
        //    PhotonNetwork.Disconnect();
        //    PhotonManager.offlineMode = true;
        //    //GetComponent<PhotonObjects>().Destroy();
        //}
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("ActivateGuideRPC", RpcTarget.AllBuffered, id);
    }
    [PunRPC]
    void ActivateGuideRPC(int id)
    {
        Debug.Log("ActivateGuide_RPC");
        if (PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            guideSystem.GuidePanelActivate(id);
        }
    }

  

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("DISCONNECT SERVER");

    }
}
