using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    //[SerializeField] private GameObject dialogeDisable;
<<<<<<< Updated upstream
    [SerializeField] private List<GameObject> Dialogs;
=======
    public List<GameObject> Dialogs;
>>>>>>> Stashed changes
    public List<bool> DialogueComplete;
  
    public bool areaDialoguePatient;
    public bool areaDialogueDoctor;
    bool pause;
    void Start()
    {
        for (int i = 0; i < Dialogs.Count; i++) Dialogs[i].SetActive(false);
        for (int i = 0; i < DialogueComplete.Count; i++) DialogueComplete[i]=false;
    }

    public void ActivateDialogue (int Number) 
    {
        Dialogs[Number].SetActive(true);
    }

    //public void StartDialoguePatient() 
    //{
    //    if (!DialogueComplete[1])
    //    {
    //        Dialogs[0].SetActive(true);
    //        return;
    //    }
    //    if (DialogueComplete[1] && !DialogueComplete[2])
    //    {
    //        Dialogs[1].SetActive(true);
    //        return;
    //    }
    //    else
    //    {
    //        dialogeDisable.SetActive(true);
    //        return;
    //    }
    //}

    //public void StartDialogueDoctor()
    //{
    //    if (DialogueComplete[0] && DialogueComplete[1] && !DialogueComplete[2])
    //    {
    //        Dialogs[3].SetActive(true);
    //    }
    //}

    public void StartDialogue() 
    {
        if (areaDialoguePatient)
        {
            if (!DialogueComplete[0])
            {
                Dialogs[0].SetActive(true);
                return;
            }
            if (DialogueComplete[0] && !pause)
            {
                pause=true;
                return;
            }
            if (pause && !DialogueComplete[1])
            {
                Dialogs[1].SetActive(true);
                return;
            }

            //else
            //{
            //    dialogeDisable.SetActive(true);
            //    return;
            //}
        }


        if (areaDialogueDoctor)
        {
            if (DialogueComplete[1] && !DialogueComplete[2])
            {
                Dialogs[2].SetActive(true);
            }
            //else
            //{
            //    dialogeDisable.SetActive(true);
            //    return;
            //}
        }
       
        

    }
    public void inAreaDialoguePatient()
    {
        areaDialoguePatient = true;
    }
    public void outAreaDialoguePatient()
    {
        areaDialoguePatient = false;
    }
    public void inAreaDialogueDoctor() 
    {
        areaDialogueDoctor = true; 
    }
    public void outAreaDialogueDoctor()
    {
        areaDialogueDoctor = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
