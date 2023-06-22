using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TabletBulletListPhoton : MonoBehaviour
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
            TabletBulletList tabletBulletList = FindObjectOfType<TabletBulletList>();
            tabletBulletList.crossOut += CrossOut;
        }
    }

    void CrossOut(int ID) 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("CrossOutRPC", RpcTarget.AllBuffered, ID);
    }
    [PunRPC]
    void CrossOutRPC(int id) 
    {
        Debug.Log("CrossEvent_RPC");
        if (PhotonManager._viewerApp)
        {
            TabletBulletList tabletBulletList = FindObjectOfType<TabletBulletList>();
            tabletBulletList.CrossOut(id);
        }
    }
 
}
