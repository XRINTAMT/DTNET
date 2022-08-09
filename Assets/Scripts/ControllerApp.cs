using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerApp : MonoBehaviour
{
    [SerializeField] private AnimationsController animationsController;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField]
    private bool patientSittingDownAnimation1, patientLayingAnimation1, patientSittingDownAnimation2, patientLayingAnimation2, putOnShirt, putOffShirt,
        callDoctorAdamsAnimation, doctorInspectDefebrillatorAnimation, nurseAnimation, dialoguWithPatient1, dialoguWithPatient2;
    public void PatientSittingDownAnimation1() 
    {
        animationsController.AnimationSeatDownPatient1();
    }
    public void PatientLayingAnimation1()
    {
        animationsController.AnimationLayingPatient1();
    }
    public void PatientSittingDownAnimation2()
    {
        animationsController.AnimationSeatDownPatient2();
    }
    public void PatientLayingAnimation2()
    {
        animationsController.AnimationLayingPatient2();
    }
    public void CallDoctorAdamsAnimation()
    {
        animationsController.AnimationArriveDoctor();
    }
    public void DoctorInspectDefebrillatorAnimation()
    {
        animationsController.AnimationWalkDoctor();
    }
    public void NurseAnimation()
    {
        animationsController.AnimationNurseTakeInject();
    }

    public void DialoguWithPatient1()
    {
        dialogueSystem.Dialogs[0].SetActive(true);
    }

    public void DialoguWithPatient2()
    {
        dialogueSystem.Dialogs[1].SetActive(true);
    }

    public void DialoguWithPatient3()
    {
        dialogueSystem.Dialogs[1].SetActive(true);
    }

    public void PutOnShirt()
    {
        animationsController.PutOnShirt();
    }
    public void PutOffShirt()
    {
        animationsController.PutOffShirt();
    }
    void Update()
    {
        if (patientSittingDownAnimation1)
        {
            PatientSittingDownAnimation1();
            patientSittingDownAnimation1 = false;
        }
        if (patientLayingAnimation1)
        {
            PatientLayingAnimation1();
            patientLayingAnimation1 = false;
        }
        if (patientSittingDownAnimation2)
        {
            PatientSittingDownAnimation2();
            patientSittingDownAnimation2 = false;
        }
        if (patientLayingAnimation2)
        {
            PatientLayingAnimation2();
            patientLayingAnimation2 = false;
        }
        if (callDoctorAdamsAnimation)
        {
            CallDoctorAdamsAnimation();
            callDoctorAdamsAnimation = false;
        }
        if (doctorInspectDefebrillatorAnimation)
        {
            DoctorInspectDefebrillatorAnimation();
            doctorInspectDefebrillatorAnimation = false;
        }
        if (nurseAnimation)
        {
            NurseAnimation();
            nurseAnimation = false;
        }
        if (dialoguWithPatient1)
        {
            DialoguWithPatient1();
            dialoguWithPatient1 = false;
        }
        if (dialoguWithPatient2)
        {
            DialoguWithPatient2();
            dialoguWithPatient2 = false;
        }
        if (putOnShirt)
        {
            PutOnShirt();
            putOnShirt = false;
        }
        if (putOffShirt)
        {
            PutOffShirt();
            putOffShirt = false;
        }

    }
}
