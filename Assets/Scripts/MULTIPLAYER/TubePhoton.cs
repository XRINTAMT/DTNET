using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class TubePhoton : MonoBehaviour
{
    public Packaging[] packagings;
    //public Packaging[] packagingsTube;
    //public Packaging[] packagingsSyringe;
    public List <Packaging> packagingsTube = new List<Packaging>();
    public List<Packaging> packagingsSyringe = new List<Packaging>();

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
            InfiniteBox [] infiniteBox = FindObjectsOfType<InfiniteBox>();
            for (int i = 0; i < infiniteBox.Length; i++)
            {
                infiniteBox[i].instNewPackage += InstNewObj;
            }

            pump_ConnectTubing.connectTubing += ConnectTubing;
        }
        if (PhotonManager._viewerApp)
        {
            SpawnableLeftTheAreaTrigger [] spawnableLeftTheAreaTrigger = FindObjectsOfType<SpawnableLeftTheAreaTrigger>();
            for (int i = 0; i < spawnableLeftTheAreaTrigger.Length; i++)
                Destroy(spawnableLeftTheAreaTrigger[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InstNewObj(string name, int viewId, string date) 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("InstNewObjRPC", RpcTarget.AllBuffered, name, viewId, date);
        }
    }
    void ConnectTubing(GameObject tubing)
    {
        if (!PhotonManager._viewerApp && !PhotonManager.offlineMode)
        {
            GetComponent<PhotonView>().RPC("ConnectTubingRPC", RpcTarget.All, tubing.GetComponent<PhotonView>().ViewID);
        }
    }
    [PunRPC]
    void InstNewObjRPC(string nameObj, int viewId, string date)
    {
        Debug.Log("InstNewObjRPC");

        Array.Clear(packagings, 0, packagings.Length);
        packagingsTube.Clear();
        packagingsSyringe.Clear();

        packagings = FindObjectsOfType<Packaging>();

        for (int i = 0; i < packagings.Length; i++)
        {
            if (packagings[i].name.Contains("Tubing(Packaged)"))
                packagingsTube.Add(packagings[i]);

            if (packagings[i].name.Contains("Syringe(Packaged)"))
                packagingsSyringe.Add(packagings[i]);
        }

        if (nameObj.Contains("Tubing(Packaged)"))
        {
            for (int i = 0; i < packagingsTube.Count; i++)
            {
                if (packagingsTube[i].GetComponent<PhotonView>().ViewID== viewId)
                    packagingsTube[i].GetComponent<ExpirationDate>().DateStamp.text = date;
            }

            if (PhotonManager._viewerApp)
            {
                for (int i = 0; i < packagingsTube.Count; i++)
                {
                    foreach (Rigidbody rb in packagingsTube[i].GetComponentsInChildren<Rigidbody>())
                    {
                        rb.isKinematic = true;

                        if (packagingsTube[i].Content.GetComponent<Joint>())
                            Destroy(packagingsTube[i].Content.GetComponent<Joint>());
                        if (packagingsTube[i].RemovablePart.GetComponent<Joint>())
                            Destroy(packagingsTube[i].RemovablePart.GetComponent<Joint>());
                        if (packagingsTube[i].Package.GetComponent<Joint>())
                            Destroy(packagingsTube[i].Package.GetComponent<Joint>());
                    }
                    packagingsTube[i].Content.transform.parent = null;
                    packagingsTube[i].RemovablePart.transform.parent = null;
                    packagingsTube[i].Package.transform.parent = null;
                }
            }
        }


        if (nameObj.Contains("Syringe(Packaged)"))
        {
            for (int i = 0; i < packagingsSyringe.Count; i++)
            {
                if (packagingsSyringe[i].GetComponent<PhotonView>().ViewID == viewId)
                    packagingsSyringe[i].GetComponent<ExpirationDate>().DateStamp.text = date;
            }

            if (PhotonManager._viewerApp)
            {
                for (int i = 0; i < packagingsSyringe.Count; i++)
                {

                    foreach (Rigidbody rb in packagingsSyringe[i].GetComponentsInChildren<Rigidbody>())
                    {
                        rb.isKinematic = true;

                        if (packagingsSyringe[i].Content.GetComponent<Joint>())
                            Destroy(packagingsSyringe[i].Content.GetComponent<Joint>());
                        if (packagingsSyringe[i].RemovablePart.GetComponent<Joint>())
                            Destroy(packagingsSyringe[i].RemovablePart.GetComponent<Joint>());
                        if (packagingsSyringe[i].Package.GetComponent<Joint>())
                            Destroy(packagingsSyringe[i].Package.GetComponent<Joint>());
                    }

                    packagingsSyringe[i].Content.transform.parent = null;
                    packagingsSyringe[i].RemovablePart.transform.parent = null;
                    packagingsSyringe[i].Package.transform.parent = null;
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
            for (int i = 0; i < packagingsTube.Count; i++)
            {
                packagingsTube[i].gameObject.SetActive(false);
            }
        }
    }

}
