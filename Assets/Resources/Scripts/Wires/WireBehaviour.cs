using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SplineMesh;

public class WireBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] EndPoint;
    [SerializeField] GameObject[] EndSegment;
    [SerializeField] GameObject SegmentsStorage;

    // Start is called before the first frame update
    void Start()
    {

        EndSegment = new GameObject[2];
        WireBuilder wirebuilder = GetComponentInChildren<WireBuilder>();
        EndSegment[0] = wirebuilder.wayPoints[0];
        EndSegment[1] = wirebuilder.wayPoints.Last();
        /*
        EndSegment[0] = SegmentsStorage.transform.GetChild(0).gameObject;
        Debug.Log(EndSegment[0]);
        EndSegment[1] = SegmentsStorage.transform.GetChild(SegmentsStorage.transform.childCount-1).gameObject;
        Debug.Log(EndSegment[1]);
        */
        for (int i = 0; i < 2; i++){
            if (EndPoint[i] != null)
            {
                EndSegment[i].GetComponent<Rigidbody>().position = EndPoint[i].position;
                EndSegment[i].transform.position = EndPoint[i].position;
                EndSegment[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                EndSegment[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
