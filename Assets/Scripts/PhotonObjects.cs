using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonObjects : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

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
            //for (int i = 0; i < objects.Length; i++)
            //{
            //    Destroy(objects[i].GetComponent<PhotonView>());
            //    Destroy(objects[i].GetComponent<PhotonTransformView>());
            //}
            //Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                objects[i].GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
