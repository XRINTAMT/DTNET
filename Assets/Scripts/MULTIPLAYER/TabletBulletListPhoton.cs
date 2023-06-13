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
            TabletBulletList tabletBulletList = GetComponent<TabletBulletList>();
            if (tabletBulletList)
            {
                Debug.Log(66);
            }
            //tabletBulletList.crossOut += CrossOut;
        }
    }

    void CrossOut(int ID) 
    {
        GetComponent<PhotonView>().RPC("CrossOutRPC", RpcTarget.OthersBuffered, ID);
    }
    [PunRPC]
    void CrossOutRPC(int id) 
    {
        if (PhotonManager._viewerApp)
        {
            TabletBulletList tabletBulletList = GetComponent<TabletBulletList>();
            tabletBulletList.CrossOut(id);
        }
    }
 
}
