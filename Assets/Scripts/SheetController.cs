using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.UI;

public class SheetController : MonoBehaviour
{

    Camera cam;
    Vector3 startPos;
    Quaternion startRot;
    Transform startParent;
    Rigidbody rb;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject areaLimit;
    [SerializeField] GameObject modelCollider;
    public bool inHead = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        startParent = transform.parent;
        startPos = transform.localPosition;
        startRot = transform.localRotation;
      
    }

    public void Grab() 
    {
        if (inHead)
        {
            GetComponent<Grabbable>().parentOnGrab = false;
            rb.isKinematic = false;
            transform.parent = cam.transform;
            transform.localPosition = new Vector3(0, 0, 0.5f);
            transform.localRotation = Quaternion.Euler(0,0,0);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            canvas.GetComponent<GraphicRaycaster>().enabled = true;
            buttonExit.SetActive(true);

            modelCollider.layer = 16;

            if (areaLimit != null) areaLimit.SetActive(true);
           
        }
        if (!inHead)
        {
            GetComponent<Grabbable>().parentOnGrab = true;
            modelCollider.layer = 10;
            rb.isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = 0;
        }
    }
 
    public void Exit()
    {
        rb.isKinematic = true;
        transform.parent = startParent;
        transform.localPosition = startPos;
        transform.localRotation = startRot;

        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        buttonExit.SetActive(false);
    }

 
    public void Realesee()
    {
        if (inHead)
        {
            modelCollider.layer = 16;
            rb.isKinematic = true;
            transform.parent = cam.transform;
        }
        if (!inHead)
        {
            modelCollider.layer = 10;
        }
    }
  
    void Update()
    {

    }
   
}