using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class NewSyringeMechanic : MonoBehaviour
{
    [SerializeField] GameObject piston;
    GameObject bottle;
    ConfigurableJoint configurableJointPiston;
    Grabbable grabbableSyringe;
    Grabbable grabbablePiston;
    bool inBottle;
    bool updatePistonPos;
    Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        grabbableSyringe= GetComponent<Grabbable>();
        grabbableSyringe.onGrab.AddListener(GrabSyringe);
        grabbableSyringe.onRelease.AddListener(ReleaseSyringe);

        grabbablePiston = piston.GetComponent<Grabbable>();
        grabbablePiston.onGrab.AddListener(UpdatePistonPos);
        grabbablePiston.onRelease.AddListener(StopUpdatePistonPos);
        configurableJointPiston = piston.GetComponent<ConfigurableJoint>();
    }

    void UpdatePistonPos(Hand hand, Grabbable grabbable) 
    {
        updatePistonPos = true;
        //if (grabbablePiston.GetComponent<FixedJoint>())
        //    Destroy(grabbablePiston.GetComponent<FixedJoint>());
    }
    void StopUpdatePistonPos(Hand hand, Grabbable grabbable)
    {
        updatePistonPos = false;
        grabbablePiston.gameObject.AddComponent<FixedJoint>();
        grabbablePiston.GetComponent<FixedJoint>().connectedBody = grabbableSyringe.GetComponent<Rigidbody>();
        grabbablePiston.GetComponent<FixedJoint>().breakForce = 300;
    }

    

    void GrabSyringe(Hand hand, Grabbable grabbable) 
    {
        this.hand = hand;

        if (GetComponent<FixedJoint>())
            Destroy(GetComponent<FixedJoint>());

        if (GetComponent<ConfigurableJoint>())
            Destroy(GetComponent<ConfigurableJoint>());

        grabbablePiston.enabled = false;
    }
    void ReleaseSyringe(Hand hand, Grabbable grabbable) 
    {
        if (inBottle)
        {
            gameObject.AddComponent<FixedJoint>();
            GetComponent<FixedJoint>().connectedBody = bottle.GetComponent<Rigidbody>();
            transform.parent = bottle.transform;
            grabbablePiston.enabled = true;
        }
        if (!inBottle)
        {
            grabbablePiston.enabled = false;
        }

    }


    public void Vibration(int time) 
    {
        //hand.PlayHapticVibration(time);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Indicate")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            inBottle = true;
        }
        if (other.tag == "AreaLimit")
        {
            bottle = other.transform.parent.gameObject;
            grabbableSyringe.GetComponent<Stabber>().enabled = true;
        }
        //if (other.tag == "AreaLimit")
        //{
        //    grabbableSyringe.GetComponent<Stabber>().enabled = false;
        //}

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Indicate")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            inBottle = false;
        }
        if (other.tag == "AreaLimit")
        {
            bottle = null;
            grabbableSyringe.GetComponent<Stabber>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (updatePistonPos)
        {
            configurableJointPiston.targetPosition = new Vector3(piston.transform.localPosition.y / -20, 0, 0);
        }
    }
}
