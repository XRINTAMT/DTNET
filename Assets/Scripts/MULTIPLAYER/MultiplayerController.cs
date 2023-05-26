using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class MultiplayerController : MonoBehaviour
{
    [SerializeField] Transform headFollow;
    [SerializeField] Transform rightHandFollow;
    [SerializeField] Transform leftHandFollow;

    [SerializeField] Transform headFollower;
    [SerializeField] Transform rightHandFollower;
    [SerializeField] Transform leftHandFollower;

    void Start()
    {
        headFollow = FindObjectOfType<AutoHandPlayer>().headCamera.transform;
        rightHandFollow = FindObjectOfType<AutoHandPlayer>().handRight.transform;
        leftHandFollow = FindObjectOfType<AutoHandPlayer>().handLeft.transform;
    }

    // Update is called once per frame
    void Update()
    {
        headFollower.transform.position = headFollow.transform.position;
        headFollower.transform.rotation = headFollower.transform.rotation;

        rightHandFollower.transform.position = rightHandFollow.transform.position;
        rightHandFollower.transform.rotation = rightHandFollow.transform.rotation;

        leftHandFollower.transform.position = leftHandFollow.transform.position;
        leftHandFollower.transform.rotation = leftHandFollow.transform.rotation;
    }
}
