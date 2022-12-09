using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrabSetting : MonoBehaviour
{

    [Range(0,50)][SerializeField] private float DistanceGrab = 0.3f;
    public static float distanceGrab;
    // Start is called before the first frame update
    void Start()
    {
        distanceGrab = DistanceGrab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
