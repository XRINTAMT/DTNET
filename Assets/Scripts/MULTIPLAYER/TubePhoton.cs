using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class TubePhoton : MonoBehaviour
{
    public Packaging[] packagings;
    public Pump_ConnectTubing pump_ConnectTubing;
    public GameObject IVTube; 
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

        pump_ConnectTubing = FindObjectOfType<Pump_ConnectTubing>();
        IVTube = FindObjectOfType<Pump_ConnectTubing>().IVTube;

        if (!PhotonManager._viewerApp)
        {
            InfiniteBox infiniteBox = FindObjectOfType<InfiniteBox>();
            infiniteBox.instNewPackage += InstNewPackage;

            pump_ConnectTubing.connectTubing += ConnectTubing;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InstNewPackage() 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("InstNewPackageRPC", RpcTarget.AllBuffered);
        }

    }
    void ConnectTubing(GameObject tubing)
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("ConnectTubingRPC", RpcTarget.All, tubing.GetComponent<PhotonView>().ViewID);
        }

    }
    [PunRPC]
    void InstNewPackageRPC()
    {
        Debug.Log("InstNewPackageRPC");

        Array.Clear(packagings, 0, packagings.Length);
        packagings = FindObjectsOfType<Packaging>();



        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < packagings.Length; i++)
            {
                foreach (Rigidbody rb in packagings[i].GetComponentsInChildren<Rigidbody>())
                {
                    rb.isKinematic = true;
                    Packaging instPackaching = packagings[i];
                    if (packagings[i].Content.GetComponent<Joint>())
                    {
                        Destroy(packagings[i].Content.GetComponent<Joint>());
                    }
                    if (packagings[i].RemovablePart.GetComponent<Joint>())
                    {
                        Destroy(packagings[i].RemovablePart.GetComponent<Joint>());
                    }
                    if (packagings[i].Package.GetComponent<Joint>())
                    {
                        Destroy(packagings[i].Package.GetComponent<Joint>());
                    }
                }
            }
        }
    }

    [PunRPC]
    void ConnectTubingRPC(int viewId)
    {
        Debug.Log("ConnectTubingRPC" +viewId);

      
        if (PhotonManager._viewerApp)
        {
            IVTube.SetActive(true);
            for (int i = 0; i < packagings.Length; i++)
            {

                packagings[i].gameObject.SetActive(false);
                

            }

           
        }
    }

}
