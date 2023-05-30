using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class SpawnMultiplayer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp)
        {
            PhotonNetwork.Instantiate("Multipalyer", Vector3.zero, Quaternion.identity);
        }
        if (PhotonManager._viewerApp)
        {
            AutoHandPlayer autoHandPlayer = FindObjectOfType<AutoHandPlayer>();
            autoHandPlayer.headCamera.gameObject.AddComponent<ViewerController>();
            autoHandPlayer.GetComponent<Rigidbody>().isKinematic = true;
            foreach (Renderer rend in autoHandPlayer.transform.root.GetComponentsInChildren<Renderer>())
            {
                rend.enabled = false;
            }

            Destroy(FindObjectOfType<FadeWall>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
