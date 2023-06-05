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
    [SerializeField] Transform startParent;
    public Rigidbody rb;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject areaLimit;
    [SerializeField] GameObject modelCollider;
    [SerializeField] float scaleFactor;
    public GameObject keyboard;
    public bool inHead = true;
    public bool interpolation;
    public bool grab;
    public bool onPlace = true;

    Vector3 interpolatePos;
    bool onTrigger;
    public GameObject objChangeScale;



    bool leftHand;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        PlayerObject _player = transform.GetComponentInParent<PlayerObject>();
        if (startParent == null)
        {
            startParent = transform.parent;
            startPos = transform.localPosition;
            startRot = transform.localRotation;
        }
        GetComponent<Grabbable>().enabled = true;

        if (_player != null)
        {
            Debug.Log("Player is not null!");
            transform.parent = _player.GetComponentInChildren<HipsEstimation>().transform;
            _player.LeftHand.GetComponent<Hand>().Unsqueeze();
            _player.LeftHand.GetComponent<Hand>().ForceReleaseGrab();
            _player.RightHand.GetComponent<Hand>().Unsqueeze();
            _player.RightHand.GetComponent<Hand>().ForceReleaseGrab();
            Realesee();
        }

    }

    public void Awake()
    {
        
        /*
        Hand _hnd;
        if(transform.parent.TryGetComponent<Hand>(out _hnd))
        {
            GetComponent<Grabbable>().enabled = true;
            _hnd.TryGrab(GetComponent<Grabbable>());
        }
        */
    }

    public void ChangeScale(GameObject obj) 
    {
        objChangeScale = obj;

    }
    public void Grab()
    { 


        //if (objChangeScale != null) objChangeScale.transform.localScale = new Vector3(1, 1f, 1f);
        if (objChangeScale != null) objChangeScale.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        grab = true;
        if (inHead)
        {
            interpolation = false;
            //GetComponent<Grabbable>().parentOnGrab = false;
            rb.isKinematic = false;
            GetComponent<Grabbable>().enabled = false;
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
        if (keyboard != null) keyboard.SetActive(false);
    }
 
    public void Exit()
    {
        if(GetComponentInParent<PlacePoint>() != null)
        {
            return;
        }
        if (rb!=null) rb.isKinematic = true;
        transform.parent = startParent;
        transform.localPosition = startPos;
        transform.localRotation = startRot;
        if (areaLimit != null) areaLimit.SetActive(false);
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        buttonExit.SetActive(false);
        onPlace = true;
        if (objChangeScale != null) objChangeScale.transform.localScale = new Vector3(1, 1f, 1f);
        if (keyboard != null) keyboard.SetActive(false);
        FadeUI _f;
        if (TryGetComponent<FadeUI>(out _f))
        {
            _f.FadeIn();
        }
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
            GetComponent<Grabbable>().enabled = true;
            if (objChangeScale != null) objChangeScale.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
         
            if (body != null)
            {
                transform.parent = body.transform;
                if (areaLimit != null) areaLimit.SetActive(false);
            }
            if (onTrigger)
            {
                Exit();
            }
            else
            {
                FadeUI _f;
                if (TryGetComponent<FadeUI>(out _f))
                {
                    _f.FadeOut();
                }
            }
        }

        if (!inHead)
        {
            modelCollider.layer = 10;
            rb.isKinematic = true;
            if (objChangeScale!=null) objChangeScale.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            FadeUI _f;
            if (TryGetComponent<FadeUI>(out _f))
            {
                _f.FadeOut();
            }
        }
        if (keyboard != null) keyboard.SetActive(false);
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



    void Update()
    {
        if (transform.localPosition != interpolatePos & interpolation)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, interpolatePos, 10);
        }
    }
   
}
