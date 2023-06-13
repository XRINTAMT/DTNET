using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class MultiplayerController : MonoBehaviour
{
    PlayerMode playerMode;

    [SerializeField] Transform headFollow;
    [SerializeField] Transform rightHandFollow;
    [SerializeField] Transform leftHandFollow;
    
    public Transform headFollower;
    public Transform rightHandFollower;
    public Transform leftHandFollower;

    void Start()
    {
        if (playerMode==PlayerMode.Player)
        {
            headFollow = FindObjectOfType<AutoHandPlayer>().headCamera.transform;
            rightHandFollow = FindObjectOfType<AutoHandPlayer>().handRight.transform;
            leftHandFollow = FindObjectOfType<AutoHandPlayer>().handLeft.transform;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        headFollower.transform.position = headFollow.transform.position;
        headFollower.transform.rotation = headFollow.transform.rotation;

        rightHandFollower.transform.position = rightHandFollow.transform.position;
        rightHandFollower.transform.rotation = rightHandFollow.transform.rotation;

        leftHandFollower.transform.position = leftHandFollow.transform.position;
        leftHandFollower.transform.rotation = leftHandFollow.transform.rotation;
    }

}

public enum PlayerMode 
{
    Player,
    Viewer
}
