using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetController : MonoBehaviour
{
    // Start is called before the first frame update

    Camera cam;
    Transform startPos;
    Transform startParent;
    void Start()
    {
        cam = Camera.main;
        startParent = transform.parent;
        startPos = transform;
    }

    public void Grab() 
    {
        if (cam!=null)
        {
            transform.parent = cam.transform;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public void Release()
    {
        transform.parent = startParent;
        transform.localPosition = startPos.position;
        transform.localRotation = startPos.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
