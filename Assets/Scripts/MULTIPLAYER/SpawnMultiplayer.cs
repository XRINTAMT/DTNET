using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class SpawnMultiplayer : MonoBehaviour
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
            PhotonNetwork.Instantiate("Multiplayer", Vector3.zero, Quaternion.identity);
        }
        if (PhotonManager._viewerApp)
        {
            AutoHandPlayer autoHandPlayer = FindObjectOfType<AutoHandPlayer>();
            autoHandPlayer.headCamera.gameObject.AddComponent<PlayerViewerMovement>();
            autoHandPlayer.GetComponent<Rigidbody>().isKinematic = true;
            foreach (Renderer rend in autoHandPlayer.transform.parent.GetComponentsInChildren<Renderer>())
            {
                rend.enabled = false;
            }
            autoHandPlayer.headCamera.GetComponent<TrackedPoseDriver>().enabled = false;
            autoHandPlayer.handLeft.follow.parent.GetComponent<TrackedPoseDriver>().enabled = false;
            autoHandPlayer.handRight.follow.parent.GetComponent<TrackedPoseDriver>().enabled = false;
            Destroy(FindObjectOfType<FadeWall>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
