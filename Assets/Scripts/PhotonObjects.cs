using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PhotonObjects : MonoBehaviour
{
    [SerializeField] PhotonTransformView[] transfornViewObj;
    [SerializeField] TextMeshProUGUI roomNumber;
    [SerializeField] TextMeshProUGUI roomRegion;
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

            Destroy(gameObject);
        }

        if (!PhotonManager.offlineMode)
        {
            FindObjectOfType<RestartSystem>().enabled = false;
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
                if (transfornViewObj[i].GetComponent<Rigidbody>())
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


        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        roomRegion.text = PhotonNetwork.CloudRegion;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
