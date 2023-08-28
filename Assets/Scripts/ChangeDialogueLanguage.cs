using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumDialogue;
using System;

[RequireComponent(typeof(QD_DialogueHandler))]
public class ChangeDialogueLanguage : MonoBehaviour
{
    [Serializable]
    struct LangDialouge
    {
        public string Language;
        public QD_Dialogue Dialogue;
        public QD_Dialogue MaleDialogue;
    }

    [SerializeField] LangDialouge[] Dialogues;

    void Awake()
    {
        string lang = PlayerPrefs.GetString("Language", "English");
        for(int i = 0; i < Dialogues.Length; i++)
        {
            if(lang == Dialogues[i].Language)
            {
                if(PlayerPrefs.GetInt("Gender") == 0)
                {
                    GetComponent<QD_DialogueHandler>().dialogue = Dialogues[i].Dialogue;
                }
                else
                {
                    GetComponent<QD_DialogueHandler>().dialogue = Dialogues[i].MaleDialogue;
                }
                GetComponent<QuantumTek.QuantumDialogue.Demo.QD_DialogueDemo>().SetText();
                return;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
