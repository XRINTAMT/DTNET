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
        if (cam!=null)
        {
            rb.isKinematic = false;
            transform.parent = cam.transform;
            transform.localPosition = new Vector3(0, 0, 0.5f);
            transform.localRotation = Quaternion.Euler(0,0,0);

            canvas.GetComponent<GraphicRaycaster>().enabled = true;
            buttonExit.SetActive(true);
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
        rb.isKinematic = true;
        transform.parent = cam.transform;
    }
  
    void Update()
    {
        
    }
   
}
