using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.UI;

public class SheetController : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;
    Transform startPos;
    public Vector3 startPos2;
    Transform startParent;
    Grabbable grabbable;
    HandTriggerAreaEvents handTriggerAreaEvents;
    [SerializeField] List <GameObject> sheet; 
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject buttonExit;
    [SerializeField] TypeSheet typeSheet;
    bool grab;
    
    void Start()
    {
        cam = Camera.main;
        startParent = transform.parent;
        startPos = transform;
        startPos2 = startPos.localPosition;
        grabbable = GetComponent<Grabbable>();
        handTriggerAreaEvents = GetComponent<HandTriggerAreaEvents>();
        switch (typeSheet)
        {
            case TypeSheet.ObservationSheet:
                sheet[0].SetActive(true);
                break;
            case TypeSheet.Tablet:
                sheet[1].SetActive(true);
                break;
            case TypeSheet.DoctorsAppointments:
                sheet[2].SetActive(true);
                break;
            default:
                break;
        }

    }

    public void Grab() 
    {
        if (cam!=null)
        {
            transform.parent = cam.transform;
            transform.localPosition = new Vector3(0, 0, 0.5f);
            transform.localRotation = Quaternion.Euler(0,0,0);
            grab = true;

            canvas.GetComponent<GraphicRaycaster>().enabled = true;
            buttonExit.SetActive(true);
        }
    }
    IEnumerator activateGrabbable() 
    {
        yield return new WaitForSeconds(1f);
        handTriggerAreaEvents.enabled = false;
        //grabbable.enabled = true;
    }
    public void Release()
    {
        transform.parent = startParent;
        transform.localPosition = startPos.localPosition;
        transform.localRotation = startPos.localRotation;
        grabbable.enabled = false;
        handTriggerAreaEvents.enabled = true;
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        buttonExit.SetActive(false);
    }

    public void RealeseArea() 
    {
        if (grab)
        {
            grabbable.enabled = true;
            handTriggerAreaEvents.enabled = false;
        }
    }
    public void RealeseGrabbable()
    {
        if (grab)
        {
            transform.parent = cam.transform;
        }
    }
    //public void Sqeaze()
    //{
    //    if (grab)
    //    {
    //        grabbable.enabled = true;
    //        handTriggerAreaEvents.enabled = false;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
    enum TypeSheet 
    {
        ObservationSheet,
        Tablet,
        DoctorsAppointments
    }
}
