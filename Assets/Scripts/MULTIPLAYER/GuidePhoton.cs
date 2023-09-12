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
        if (PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            //guideSystem.gameObject.SetActive(false);
            GetComponent<PhotonView>().RPC("GetGuideCanvasStateRPC", RpcTarget.MasterClient);
        }
        if (!PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            FindObjectOfType<AppManager>().activateGuideCanvas += ActivateGuideCanvas;
            guideSystem.activateGuide += ActivateGuide;

        }
        
    }


    void ActivateGuideCanvas()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("ActivateGuideCanvasRPC", RpcTarget.AllBuffered); 
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
    void GetGuideCanvasStateRPC()
    {
        Debug.Log("GetGuideCanvasStateRPC");
        GetComponent<PhotonView>().RPC("SetGuideCanvasStateRPC", RpcTarget.All, FindObjectOfType<GuideSystem>().canvas.activeSelf);
    }
    [PunRPC]
    void SetGuideCanvasStateRPC(bool state)
    {
        Debug.Log("SetGuideCanvasStateRPC");
        if (PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            guideSystem.canvas.SetActive(state);
            guideSystem.gameObject.SetActive(state);
        }
    }

    [PunRPC]
    void ActivateGuideCanvasRPC()
    {
        Debug.Log("ActivateGuideCanvas_RPC");
        if (PhotonManager._viewerApp)
        {
            GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
            guideSystem.canvas.SetActive(true);
        }
    }


    [PunRPC]
    void ActivateGuideRPC(int id)
    {
        Debug.Log("ActivateGuide_RPC");
        if (PhotonManager._viewerApp && FindObjectOfType<GuideSystem>())
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
