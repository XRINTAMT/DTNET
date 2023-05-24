using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class GrabbingDistance : MonoBehaviour
{
    private float distance;
    [SerializeField] List<Hand> hand = new List<Hand>();

    void Start()
    {
        distance = DistanceGrabSetting.distanceGrab;
        if (distance==0) distance = 0.15f;
  
        var handFind= FindObjectsOfType<Hand>();
        for (int i = 0; i < handFind.Length; i++)
        {
            hand.Add(handFind[i]);
        }
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].reachDistance = distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
