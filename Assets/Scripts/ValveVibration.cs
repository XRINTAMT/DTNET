using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class ValveVibration : MonoBehaviour
{
    Hand grabHand;
    Grabbable grabbable;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        grabbable.onGrab.AddListener(OnGrab);
        grabbable.onRelease.AddListener(OnRelease);
    }
    void OnGrab(Hand hand, Grabbable grabbable)
    {
        grabHand = hand;
    }
    void OnRelease(Hand hand, Grabbable grabbable)
    {
        grabHand = null;
    }

    public void PlayHapticVibration()
    {
        if (grabHand)
        {
            grabHand.PlayHapticVibration(0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
