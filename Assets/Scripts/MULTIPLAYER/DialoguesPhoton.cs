using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using QuantumTek.QuantumDialogue.Demo;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesPhoton : MonoBehaviour
{
    //[SerializeField] QD_DialogueDemo dialogueManager1;
    //[SerializeField] QD_DialogueDemo dialogueManager2;
    //[SerializeField] QD_DialogueDemo dialogueManager3;

    [SerializeField] QD_ChoiceButton [] buttons;

    DialogueSystem dialogueSystem;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
    }
    void Start()
    {

        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().number = 1;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().number = 2;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().number = 3;


        dialogueSystem.Dialogs[0].GetComponent<QD_DialogueDemo>().startDialogue += UpdateListButtons;
        dialogueSystem.Dialogs[1].GetComponent<QD_DialogueDemo>().startDialogue += UpdateListButtons;
        dialogueSystem.Dialogs[2].GetComponent<QD_DialogueDemo>().startDialogue += UpdateListButtons;
    }

    public void UpdateListButtons(int number) 
    {
        Array.Clear(buttons, 0, buttons.Length);
        buttons = FindObjectsOfType<QD_ChoiceButton>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].selectButton += SelectButton;
        }

        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("OpenDialogueRPC", RpcTarget.Others, number);
        }
       
    }


    [PunRPC]
    void OpenDialogueRPC(int number)
    {
        if (PhotonManager._viewerApp)
        {
            for (int i = 0; i < dialogueSystem.Dialogs.Count; i++)
            {
                if (dialogueSystem.Dialogs[i].GetComponent<QD_DialogueDemo>().number==number)
                {
                    dialogueSystem.Dialogs[i].SetActive(true);
                }
            }
        }
        gameObject.SetActive(false);
    }

    public void SelectButton(int number) 
    {
        GetComponent<PhotonView>().RPC("SelectButtonRPC", RpcTarget.Others, number);

    }


    [PunRPC]
    void SelectButtonRPC(int number)
    {
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
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
