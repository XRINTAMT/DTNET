using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.UI;

public class SheetController : MonoBehaviour
{

    Camera cam;
    [SerializeField] Transform body;
    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRot;
    Transform startParent;
    Rigidbody rb;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject areaLimit;
    [SerializeField] GameObject modelCollider;
    public bool inHead = true;
    public bool interpolation;
    public bool grab;
    public bool onPlace=true;

    Vector3 interpolatePos;
    bool onTrigger;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        startParent = transform.parent;
        if (startParent.gameObject.name.Contains("AutoHandPlayer"))
        {
            startParent = transform.parent.parent.parent;
        }
        if (!startParent.gameObject.name.Contains("(Clone)"))
        {
            startPos = transform.localPosition;
            startRot = transform.localRotation;
        }
    }

    public void Grab()
    {
        grab = true;
        if (inHead)
        {
            interpolation = false;
            //GetComponent<Grabbable>().parentOnGrab = false;
            rb.isKinematic = false;
            if (cam == null || !cam.isActiveAndEnabled)
            {
                cam = Camera.main;
            }
            //transform.parent = cam.transform;
            //transform.localPosition = new Vector3(0, 0, 0.5f);
            //transform.localRotation = Quaternion.Euler(0,0,0);
            rb.useGravity = true;
            //rb.constraints= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            canvas.GetComponent<GraphicRaycaster>().enabled = true;
            buttonExit.SetActive(true);
           
            //modelCollider.layer = 16;
            canvas.SetActive(false);

            if (areaLimit != null) areaLimit.SetActive(true);

            //if (body != null)
            //{
            //    transform.parent = body.transform;
            //    if (areaLimit != null) areaLimit.SetActive(false);
            //}
    
        }
        if (interpolation)
        {
            inHead = false;
            GetComponent<Grabbable>().parentOnGrab = false;
            rb.isKinematic = false;
            if (cam == null || !cam.isActiveAndEnabled)
            {
                cam = Camera.main;
            }
            transform.parent = cam.transform;
            transform.localPosition = new Vector3(0, 0, 0.5f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.useGravity = true;
            //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            canvas.GetComponent<GraphicRaycaster>().enabled = true;
            buttonExit.SetActive(true);

            transform.parent = startParent;
            interpolatePos = transform.localPosition;

            //modelCollider.layer = 16;
            canvas.SetActive(false);



            //if (areaLimit != null) areaLimit.SetActive(true);

        }
        if (!inHead && !interpolation)
        {
            GetComponent<Grabbable>().parentOnGrab = true;
            modelCollider.layer = 10;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.constraints = 0;
        }
        onPlace = false;
    }
 
    public void Exit()
    {
        rb.isKinematic = true;
        transform.parent = startParent;
        transform.localPosition = startPos;
        transform.localRotation = startRot;
        if (areaLimit != null) areaLimit.SetActive(false);
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        buttonExit.SetActive(false);
        onPlace = true;
    }

 
    public void Realesee()
    {
        grab = false;
        if (inHead)
        {
            //modelCollider.layer = 16;
            rb.isKinematic = true;
            transform.parent = cam.transform;
            canvas.SetActive(true);
            if (body != null)
            {
                transform.parent = body.transform;
                if (areaLimit != null) areaLimit.SetActive(false);
            }
            if (onTrigger)
            {
                Exit();
            }
        }

        if (!inHead)
        {
            modelCollider.layer = 10;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "SheetArea")
        {
            onTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "SheetArea")
        {
            onTrigger = false;
        }
    }
    //private void OnTriggerStay(Collider other)
    //{

    //    if (other.tag == "SheetArea" && !onPlace && startDetectArea)
    //    {
    //        if (grab == false) 
    //        {
    //            Exit();
    //        }

    //    }
    //}


    void Update()
    {
        if (transform.localPosition != interpolatePos & interpolation)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, interpolatePos, 10);
        }
    }
   
}
