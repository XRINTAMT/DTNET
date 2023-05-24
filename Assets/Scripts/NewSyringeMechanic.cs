using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class NewSyringeMechanic : MonoBehaviour
{
    [SerializeField] GameObject piston;
    [SerializeField] GameObject bottle;
    ConfigurableJoint configurableJointPiston;
    Grabbable grabbableSyringe;
    Grabbable grabbablePiston;
    bool inBottle;
    bool updatePistonPos;
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
    }
    void StopUpdatePistonPos(Hand hand, Grabbable grabbable)
    {
        updatePistonPos = false;
    }

    void GrabSyringe(Hand hand, Grabbable grabbable) 
    {
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
            grabbablePiston.enabled = true;
        }
        if (!inBottle)
        {
            grabbablePiston.enabled = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Indicate")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            inBottle = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Indicate")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            inBottle = false;
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
