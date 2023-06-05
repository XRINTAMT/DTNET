using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonObjects : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] PhotonTransformView[] transfornViewObj;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
        {
            PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
            for (int i = 0; i < photonViews.Length; i++)
            {
                Destroy(photonViews[i]);
            }

            PhotonTransformView[] photonTransformViews = FindObjectsOfType<PhotonTransformView>();
            for (int i = 0; i < photonTransformViews.Length; i++)
            {
                Destroy(photonTransformViews[i]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transfornViewObj = FindObjectsOfType<PhotonTransformView>();

        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < transfornViewObj.Length; i++)
            {
                transfornViewObj[i].GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (!PhotonManager._viewerApp)
        {
            for (int i = 0; i < transfornViewObj.Length; i++)
            {
                transfornViewObj[i].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }



        //if (!PhotonManager._viewerApp)
        //{
        //    for (int i = 0; i < objects.Length; i++)
        //    {
        //        objects[i].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        //        objects[i].GetComponent<Rigidbody>().isKinematic = true;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
