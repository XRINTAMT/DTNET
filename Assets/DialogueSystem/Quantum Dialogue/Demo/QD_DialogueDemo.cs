﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using ScenarioTaskSystem;

namespace QuantumTek.QuantumDialogue.Demo
{
    public class QD_DialogueDemo : MonoBehaviour
    {
        public QD_DialogueHandler handler;
        public Text speakerName;
        public Text messageText;
        public Transform choices;
        public TextMeshProUGUI choiceTemplate;

        public string nameDialog;

        private List<TextMeshProUGUI> activeChoices = new List<TextMeshProUGUI>();
        private List<TextMeshProUGUI> inactiveChoices = new List<TextMeshProUGUI>();

        private bool ended;
        AudioSource audioSource;
        [SerializeField] DialogueSystem dialogueSystem;
        [SerializeField] GameObject panelUi;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            handler.SetConversation(nameDialog);
            SetText();
        }



        private void Update()
        {
            // Don't do anything if the conversation is over
            if (ended)
                return;

            // Check if the space key is pressed and the current message is not a choice
            if (handler.currentMessageInfo.Type == QD_NodeType.Message && Input.GetKeyUp(KeyCode.Space))
                Next();
        }


        public void NextTextDialogue() 
        {
            if (handler.currentMessageInfo.Type == QD_NodeType.Message)
                Next();
        }
        private void ClearChoices()
        {
            for (int i = activeChoices.Count - 1; i >= 0; --i)
            {
                // Use object pooling with the choices to prevent unecessary garbage collection
                activeChoices[i].gameObject.SetActive(false);
                activeChoices[i].text = "";
                inactiveChoices.Add(activeChoices[i]);
                activeChoices.RemoveAt(i);
            }
        }

        private void GenerateChoices()
        {
            // Exit if not a choice
            if (handler.currentMessageInfo.Type != QD_NodeType.Choice)
                return;
            // Clear the old choices
            ClearChoices();
            // Generate new choices
            QD_Choice choice = handler.GetChoice();
            int added = 0;
            // Use inactive choices instead of making new ones, if possible
            while (inactiveChoices.Count > 0 && added < choice.Choices.Count)
            {
                int i = inactiveChoices.Count - 1;
                TextMeshProUGUI cText = inactiveChoices[i];
                cText.text = choice.Choices[added];
                QD_ChoiceButton button = cText.GetComponent<QD_ChoiceButton>();
                button.number = added;
                cText.gameObject.SetActive(true);
                activeChoices.Add(cText);
                inactiveChoices.RemoveAt(i);
                added++;
            }
            // Make new choices if any left to make
            while (added < choice.Choices.Count)
            {
                TextMeshProUGUI newChoice = Instantiate(choiceTemplate, choices);
                newChoice.text = choice.Choices[added];
                QD_ChoiceButton button = newChoice.GetComponent<QD_ChoiceButton>();
                button.number = added;
                newChoice.gameObject.SetActive(true);
                activeChoices.Add(newChoice);
                added++;
            }
        }

        private void SetText()
        {
            // Clear everything
            speakerName.text = "";
            messageText.gameObject.SetActive(false);
            panelUi.SetActive(false);
            messageText.text = "";
            ClearChoices();

            // If at the end, don't do anything
            if (ended)
                return;

            // Generate choices if a choice, otherwise display the message
            if (handler.currentMessageInfo.Type == QD_NodeType.Message)
            {
                QD_Message message = handler.GetMessage();
                speakerName.text = message.SpeakerName;
                messageText.text = message.MessageText;

                Debug.Log("speakMessage");
                audioSource.clip = message.Clip;
                audioSource.Play();
                panelUi.SetActive(true);
                messageText.gameObject.SetActive(true);

            }
            else if (handler.currentMessageInfo.Type == QD_NodeType.Choice)
            {
                speakerName.text = "Player";
                GenerateChoices();
            }
        }

        public void Next(int choice = -1)
        {
            if (ended)
            {
                Task t;
                if(TryGetComponent<Task>(out t))
                {
                    t.Complete();
                }
                return;
            }
                
            
            // Go to the next message
            handler.NextMessage(choice);
            // Set the new text
            SetText();
            // End if there is no next message
            if (handler.currentMessageInfo.ID < 0) 
            {
                if (dialogueSystem != null)
                {
                    dialogueSystem.DialogueComplete[Convert.ToInt32(nameDialog)-1] = true;
                }
                ended = true;
                Task t;
                if (TryGetComponent<Task>(out t))
                {
                    Debug.Log("Task complete: " + gameObject.name);
                    t.Complete();
                }
            }
                
        }

        public void EndDialogue(int countDialogue) 
        { 

        
        }
        public void Choose(int choice)
        {
            if (ended)
            {
                Task t;
                if (TryGetComponent<Task>(out t))
                {
                    t.Complete();
                }
                return;
            }
            Debug.Log("makeChoose");
            Next(choice);
        }
    }
}