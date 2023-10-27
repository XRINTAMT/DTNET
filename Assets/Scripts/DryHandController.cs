using System.Collections;
using System.Collections.Generic;
using Autohand;
using ScenarioTaskSystem;
using UnityEngine;
using UnityEngine.Events;

public class DryHandController : MonoBehaviour
{
    Grabbable grabbable;
    Hand handGrab;   
    public GameObject towel;
    public UnityEvent complete;
    Vector3 startPos;
    Quaternion startRot;
    Task task;
    // Start is called before the first frame update
    void Start()
    {
        task = GetComponentInParent<Task>();

        grabbable = GetComponent<Grabbable>();
        grabbable.onGrab.AddListener(OnGrab);
        grabbable.onRelease.AddListener(OnRelease);
        grabbable.onHighlight.AddListener(OnHighlight);
        grabbable.onUnhighlight.AddListener(OnUnhighlight);

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void SpawnPaper() 
    {
        GameObject paper = Instantiate(gameObject,startPos, startRot, transform.parent);
        paper.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnHighlight(Hand hand, Grabbable grabbable) 
    {
        if (!handGrab)
            towel.layer = 12;
    }
    void OnUnhighlight(Hand hand, Grabbable grabbable)
    {
        towel.layer = 0;
    }
    void OnGrab(Hand hand, Grabbable grabbable) 
    {
        handGrab = hand;
        Invoke("SpawnPaper", 0.5f);
    }
    void OnRelease(Hand hand, Grabbable grabbable)
    {
        handGrab = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hand>() && handGrab && other.GetComponent<Hand>() != handGrab) 
        {
            complete?.Invoke();
            task.Complete();
            handGrab = null;
            Debug.Log("CompleteDry");
        }

    }


}
