using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;

public class PlayerObject : MonoBehaviour
{
    [field: SerializeField] public XRHandControllerLink LeftHand { private set; get; }
    [field: SerializeField] public XRHandControllerLink RightHand { private set; get; }
    [field: SerializeField] public Camera Head { private set; get; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
