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
    [SerializeField] private Animator patientAnimator1;
    [SerializeField] private Animator patientAnimator2;
    [SerializeField] private Animator doctorAnimator;
    [SerializeField] private Animator nurseAnimator;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private GameObject ButtonStandUp;


    public void AnimationSeatDownPatient1()
    {
        animationPattientSeatDown.Play("SeatDown1");
        patientAnimator1.SetTrigger("Sitting");
        patientAnimator2.SetTrigger("Sitting");
        Invoke("activateButtonStandUp", 7f);
    }

    public void AnimationSeatDownPatient2()
    {
       
        patientAnimator1.SetTrigger("UpRight");
        patientAnimator2.SetTrigger("UpRight");
   
    }
    public void AnimationLayingPatient1()
    {
        animationPattientSeatDown.Play("SeatDown2");
        patientAnimator1.SetTrigger("Laying2");
        patientAnimator2.SetTrigger("Laying2");

    }
    public void AnimationLayingPatient2()
    {

        patientAnimator1.SetTrigger("Laying");
        patientAnimator2.SetTrigger("Laying");

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
    }

    public void AnimationArriveDoctor()
    {
        animationOpenDoor.Play("OpenDoor");
        doctorAnimator.SetTrigger("OpenDoor");

        StartCoroutine(startDialogue(2));

    }
    IEnumerator startDialogue(int number)
    {
        yield return new WaitForSeconds(8.0f);
        dialogueSystem.ActivateDialogue(number);
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

    public void AnimationStopNurse()
    {
        nurseAnimator.applyRootMotion=false;
        nurseAnimator.SetTrigger("Put Inject");
        animationDoctorNurse.Stop();
    }
    public void AnimationDoctorInspect()
    {
        //StartCoroutine(RootMotionTrue());
        //doctorAnimator.applyRootMotion = true;
        //animationDoctorWalk.Stop();
        doctorAnimator.SetTrigger("Inspect");
    }
    //IEnumerator RootMotionTrue()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //    doctorAnimator.applyRootMotion = true;
       

    //}
}
