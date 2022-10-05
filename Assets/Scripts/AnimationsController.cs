using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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



    public void animatePump() 
    {
        animationPump.Play();
    }
    public void PutOnShirt() 
    {
        MeshWithShirt.SetActive(true);
        MeshWithoutShirt.SetActive(false);
    }
    public void PutOffShirt()
    {
        MeshWithShirt.SetActive(false);
        MeshWithoutShirt.SetActive(true);
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

        StartCoroutine(startDialogue());

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
        animationDoctorNurse.Stop();
    }
    public void AnimationDoctorInspect()
    {
        doctorAnimator.SetTrigger("Inspect");
    }
   
}
