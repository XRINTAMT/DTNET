using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class AnimationsController : MonoBehaviour
{

    [SerializeField] private Animation animationPattientSeatDown;
    [SerializeField] private Animation animationDoctorWalk;
    [SerializeField] private Animation animationDoctorNurse;
    [SerializeField] private Animation animationOpenDoor;
    [SerializeField] private Animation animationPump;
    [SerializeField] private Animator patientAnimator1;
    [SerializeField] private Animator patientAnimator2;
    [SerializeField] private Animator patientAnimator3;
    [SerializeField] private Animator doctorAnimator;
    [SerializeField] private Animator nurseAnimator;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private GameObject ButtonStandUp;
    [SerializeField] private GameObject MeshWithShirt;
    [SerializeField] private GameObject MeshWithoutShirt;
    [SerializeField] private Animation syringeAnimation;
    [SerializeField] private List<PlacePoint> placePoints;
    [SerializeField] private SheetController observSheet;
    [SerializeField] private Material highlightMaterial;


    bool waterSyringe;

    public Action <bool> waterCondition;

    public Action animationSeatingDown;
    public Action animationLaying;


    public Action animationCallDoctor;
    public Action animationArrivedDoctor;
    public Action animationWalkDoctor;
    public Action animationInspectDoctor;

    public Action animationPutOffShirt;

    public Action animationWalkNurse;
    public Action animationInjectNurse;
    public Action animationStopNurse;

    public bool callDoctor;
    public bool checkDoctor;
    public bool walkDoctor;
    public bool InspectDoctor;
    private void Awake()
    {
        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < placePoints.Count; i++)
            {
                if (placePoints[i]!=null)
                {
                    placePoints[i].enabled = false;
                }
            }
        }

        if (!transform.parent.parent.name.Contains("(Clone)"))
        {
            for (int i = 0; i < placePoints.Count; i++)
            {
                //placePoints[i].enabled = false;
            }
        }
        else
        {
            Debug.Log("Just loaded, gotta remove some highlights");
            for (int i = 0; i < placePoints.Count; i++)
            {
                if (placePoints[i].GetComponentInChildren<Sensor>() != null)
                {
                    MeshRenderer[] highlights = placePoints[i].GetComponentsInChildren<MeshRenderer>();
                    //Debug.Log("Mesh renderers in " + placePoints[i] + ": " + highlights.Length);
                    foreach(MeshRenderer highlight in highlights)
                    {
                        if(highlight.material.color == highlightMaterial.color)
                        {
                            highlight.gameObject.SetActive(false);
                            Debug.Log("Removed this highlight");
                        }
                        else
                        {
                            Debug.Log(highlight.material.color + " is not the same as " + highlightMaterial.color);
                        }
                    }
                }
            }
        }
    }

    public void SyringeWater() 
    {
        syringeAnimation.GetComponent<Collider>().enabled = false;
        Invoke("HaltCollider", 1);
        waterSyringe = !waterSyringe;
        if (waterSyringe)
        {
            syringeAnimation.Play("AnimationHygene");
        }
        if (!waterSyringe)
        {
            syringeAnimation.Play("HygeneOff");
        }
        waterCondition?.Invoke(waterSyringe);
    }

    void HaltCollider()
    {
        syringeAnimation.GetComponent<Collider>().enabled = true;
    }

    public void animatePump() 
    {
        animationPump.Play();
    }
    public void PutOnShirt() 
    {
        MeshWithShirt.SetActive(true);
        MeshWithoutShirt.SetActive(false);
        for (int i = 0; i < placePoints.Count; i++)
        {
            placePoints[i].enabled = false;
        }
    }
    public void PutOffShirt()
    {
        if (!PhotonManager._viewerApp)
        {
            animationPutOffShirt?.Invoke();
        }
        MeshWithShirt.SetActive(false);
        MeshWithoutShirt.SetActive(true);

        if (!PhotonManager._viewerApp)
        {
            for (int i = 0; i < placePoints.Count; i++)
            {
                if (placePoints[i]!=null)
                {
                    placePoints[i].enabled = true;
                }
               
            }
        }
       
    }

    public void AnimationSeatDownPatient1()
    {
        animationPattientSeatDown.Play("SeatDown1");
        patientAnimator1.SetTrigger("Sitting");
        patientAnimator2.SetTrigger("Sitting");
        patientAnimator3.SetTrigger("Sitting");
        Invoke("activateButtonStandUp", 7f);
    }

    public void AnimationSeatDownPatient2()
    {
        if (!PhotonManager._viewerApp)
        {
            animationSeatingDown?.Invoke();
        }

        patientAnimator1.SetTrigger("UpRight");
        patientAnimator2.SetTrigger("UpRight");
        patientAnimator3.SetTrigger("UpRight");

    }
    public void AnimationLayingPatient1()
    {
        animationPattientSeatDown.Play("SeatDown2");
        patientAnimator1.SetTrigger("Laying2");
        patientAnimator2.SetTrigger("Laying2");
        patientAnimator3.SetTrigger("Laying2");

    }
    public void AnimationLayingPatient2()
    {
        if (!PhotonManager._viewerApp)
        {
            animationLaying?.Invoke();
        }

        patientAnimator1.SetTrigger("Laying");
        patientAnimator2.SetTrigger("Laying");
        patientAnimator3.SetTrigger("Laying");

    }
    void activateButtonStandUp()
    {
        if (ButtonStandUp != null) ButtonStandUp.GetComponent<Button>().interactable = true;
    }

    public void Dialogue1Enabel()
    {


    }
    void activateButtonPatientDialogiue2()
    {
        ButtonStandUp.GetComponent<Button>().interactable = true;
    }
    public void AnimationStandUpPatient()
    {
        animationPattientSeatDown.Play("StandUp");
        patientAnimator1.SetTrigger("StandUp");
        patientAnimator2.SetTrigger("StandUp");
        patientAnimator3.SetTrigger("StandUp");
    }

    public void AnimationArriveDoctor()
    {
        //doctorAnimator.applyRootMotion = false;
        animationOpenDoor.Play("OpenDoor");
        doctorAnimator.SetTrigger("OpenDoor");
    
        if (!PhotonManager._viewerApp)
        {
            animationArrivedDoctor?.Invoke();
        }

        if (observSheet)
            observSheet.inHead = false;

        //StartCoroutine(startDialogue());

    }
    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(8.0f);
        dialogueSystem.ActivateDialogue(2);
    }

   
    public void CallMrAdams() 
    {
        if (!PhotonManager._viewerApp)
        {
            animationCallDoctor?.Invoke();
        }

        Debug.Log("ÑallDoctor");
        StartCoroutine(DoctorCome());
    }
    IEnumerator DoctorCome()
    {
        yield return new WaitForSeconds(5.0f);
        AnimationArriveDoctor();
    }
    

    public void AnimationWalkDoctor()
    {
        if (!PhotonManager._viewerApp)
        {
            animationWalkDoctor?.Invoke();
        }
        Debug.Log("walk");
        doctorAnimator.applyRootMotion = false;
        animationDoctorWalk.Play("DoctorWalk");
        doctorAnimator.SetTrigger("Walk");
    }

    public void AnimationWalkNurse()
    {
        if (!PhotonManager._viewerApp)
        {
            animationWalkNurse?.Invoke();
        }
      
        doctorAnimator.applyRootMotion = false;
        animationDoctorWalk.Play("GoToPump");
        doctorAnimator.SetTrigger("Walk");
    }

    public void AnimationNurseTakeInject()
    {
        if (!PhotonManager._viewerApp)
        {
            animationInjectNurse?.Invoke();
        }
        nurseAnimator.SetTrigger("Take Inject");
    }

    public void AnimationStopNurse()
    {
        if (!PhotonManager._viewerApp)
        {
            animationStopNurse?.Invoke();
        }
        //nurseAnimator.applyRootMotion=false;
        nurseAnimator.SetTrigger("Put Inject");
        //animationDoctorNurse.Stop();
    }
    public void AnimationDoctorInspect()
    {
        if (!PhotonManager._viewerApp)
        {
            animationInspectDoctor?.Invoke();
        }
        doctorAnimator.SetTrigger("Inspect");
    }

    private void Update()
    {
        if (callDoctor)
        {
            CallMrAdams();
            callDoctor = false;

        }
    }
}
