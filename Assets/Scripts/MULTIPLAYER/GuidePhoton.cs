using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GuidePhoton : MonoBehaviour
{
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

        GuideSystem guideSystem = FindObjectOfType<GuideSystem>();
        //guideSystem.gameObject.SetActive(false);
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
            guideSystem.gameObject.SetActive(false);
        }
    }

    void ActivateGuide(int id)
    {
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
}
