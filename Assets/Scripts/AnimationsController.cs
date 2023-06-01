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


    private void Awake()
    {
        if (!transform.parent.parent.name.Contains("(Clone)"))
        {
            for (int i = 0; i < placePoints.Count; i++)
            {
                placePoints[i].enabled = false;
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
        MeshWithShirt.SetActive(false);
        MeshWithoutShirt.SetActive(true);
        for (int i = 0; i < placePoints.Count; i++)
        {
            placePoints[i].enabled = true;
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
        animationOpenDoor.Play("OpenDoor");
        doctorAnimator.SetTrigger("OpenDoor");

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
        StartCoroutine(DoctorCome());
    }
    IEnumerator DoctorCome()
    {
        yield return new WaitForSeconds(5.0f);
        AnimationArriveDoctor();
    }
    
    public void AnimationWalkDoctor()
    {
        doctorAnimator.applyRootMotion = false;
        animationDoctorWalk.Play("DoctorWalk");
        doctorAnimator.SetTrigger("Walk");
    }

    public void AnimationWalkNurse()
    {
        doctorAnimator.applyRootMotion = false;
        animationDoctorWalk.Play("GoToPump");
        doctorAnimator.SetTrigger("Walk");
    }

    public void AnimationNurseTakeInject()
    {
        nurseAnimator.SetTrigger("Take Inject");
    }

    public void AnimationStopNurse()
    {
        //nurseAnimator.applyRootMotion=false;
        nurseAnimator.SetTrigger("Put Inject");
        //animationDoctorNurse.Stop();
    }
    public void AnimationDoctorInspect()
    {
        doctorAnimator.SetTrigger("Inspect");
    }
   
}
