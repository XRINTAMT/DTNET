using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class MultiplayerController : MonoBehaviour
{

    [SerializeField] Transform headFollow;
    [SerializeField] Transform rightHandFollow;
    [SerializeField] Transform leftHandFollow;
    
    public Transform headFollower;
    public Transform rightHandFollower;
    public Transform leftHandFollower;
    PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            headFollow = FindObjectOfType<AutoHandPlayer>().headCamera.transform;
            rightHandFollow = FindObjectOfType<AutoHandPlayer>().handRight.transform;
            leftHandFollow = FindObjectOfType<AutoHandPlayer>().handLeft.transform;
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.enabled = false;
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            headFollower.transform.position = headFollow.transform.position;
            headFollower.transform.rotation = headFollow.transform.rotation;

            rightHandFollower.transform.position = rightHandFollow.transform.position;
            rightHandFollower.transform.rotation = rightHandFollow.transform.rotation;

            leftHandFollower.transform.position = leftHandFollow.transform.position;
            leftHandFollower.transform.rotation = leftHandFollow.transform.rotation;
        }
    }

}

