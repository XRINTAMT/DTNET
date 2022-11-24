using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumDialogue;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    public QD_Dialogue dialogue;

    public List<QD_Conversation> Conversations = new List<QD_Conversation>();
    public List<QD_Speaker> Speakers = new List<QD_Speaker>();
    public List<QD_Message> Messages = new List<QD_Message>();
    public List<QD_Choice> Choices = new List<QD_Choice>();

    void Start()
    {
        for (int i = 0; i < dialogue.Conversations.Count; i++) Conversations.Add(dialogue.Conversations[i]);
        for (int i = 0; i < dialogue.Speakers.Count; i++) Speakers.Add(dialogue.Speakers[i]);
        for (int i = 0; i < dialogue.Messages.Count; i++) Messages.Add(dialogue.Messages[i]);
        for (int i = 0; i < dialogue.Choices.Count; i++) Choices.Add(dialogue.Choices[i]);
    }

    public void SetMessageText(int count,string messsageText) 
    {
        dialogue.Messages[count].MessageText = messsageText;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
