using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer Lr;
    [SerializeField] Transform[] points;
    // Start is called before the first frame update
    void Start()
    {
        Lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine() 
    {
        Lr.positionCount = points.Length;
        //this.points = points;
    
    
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Lr.SetPosition(i, points[i].position);
        }
    }
}
