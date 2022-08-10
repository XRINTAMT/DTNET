using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnTrigger : MonoBehaviour
{
    [SerializeField] private AnimationsController animationsController;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="doctor")
        {
            animationsController.AnimationDoctorInspect();
        }
        if (other.tag == "nurse")
        {
            animationsController.AnimationStopNurse();
        }
     

    }

   
}
