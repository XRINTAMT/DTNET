using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using QuantumTek.QuantumDialogue.Demo;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesPhoton : MonoBehaviour
{

    [SerializeField] QD_ChoiceButton [] buttons;

    public DialogueSystem dialogueSystem;

    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    void Start()
    {

        dialogueSystem = FindObjectOfType<DialogueSystem>(true);

        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().number = 1;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().number = 2;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().number = 3;


        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().startDialogue += OpenDialogue;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().startDialogue += OpenDialogue;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().startDialogue += OpenDialogue;

        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().generateChoices += UpdateListButtons;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().generateChoices += UpdateListButtons;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().generateChoices += UpdateListButtons;


        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().nextDialogue += Next;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().nextDialogue += Next;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().nextDialogue += Next;

        //dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().backDialogue += Back;
        //dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().backDialogue += Back;
        //dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().backDialogue += Back;
    }

    public void UpdateListButtons() 
    {
        Array.Clear(buttons, 0, buttons.Length);
        buttons = FindObjectsOfType<QD_ChoiceButton>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].selectButton += SelectButton;
        }

    }
    void OpenDialogue(int number)
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("OpenDialogueRPC", RpcTarget.All, number);
        }
    }

    public void Next(int number) 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("NextRPC", RpcTarget.All,number);
        }

    }

    public void Back(int number)
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("BackRPC", RpcTarget.All,number);
        }
    }

    public void SelectButton(int number) 
    {
        Debug.Log("SelectButtonEvent");
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("SelectButtonRPC", RpcTarget.All, number);
        }
    }

    [PunRPC]
    void OpenDialogueRPC(int number)
    {
        Debug.Log("OpenDialogueEvent_RPC");

        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < dialogueSystem.Dialogs.Count; i++)
            {
                if (dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().number == number)
                {
                    dialogueSystem.Dialogs[i].SetActive(true);
                }
            }
        }
    }


    [PunRPC]
    void SelectButtonRPC(int number)
    {
        Debug.Log("SelectButtonEvent_RPC");
        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].number==number)
                {
                    buttons[i].GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }

    [PunRPC]
    void NextRPC(int number)
    {
        Debug.Log("Next_RPC");
        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < dialogueSystem.Dialogs.Count; i++)
            {
                if (dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().number == number)
                {
                    dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().NextTextDialogue();
                }
            }
        }

    }
    void BackRPC(int number)
    {
        Debug.Log("Back_RPC");
        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < dialogueSystem.Dialogs.Count; i++)
            {
                if (dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().number == number)
                {
                    dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().BackTextDialogue();
                }
            }
        }

    }
    
}
