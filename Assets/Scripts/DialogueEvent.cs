using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumDialogue.Demo;
using System;
using ScenarioTaskSystem;
using UnityEngine.Events;

[RequireComponent(typeof(QD_DialogueDemo))]
public class DialogueEvent : MonoBehaviour
{
    [Serializable]
    struct DialogueTask
    {
        public int DialogueID;
        public bool completed;
    }

    [SerializeField] DialogueTask[] Phrases;
    [SerializeField] UnityEvent OnComplete;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool completed = true;
        for(int i = 0; i < Phrases.Length; i++)
        {
            if (Phrases[i].completed)
                continue;
            if (GetComponent<QD_DialogueDemo>().handler.currentMessageInfo.ID == Phrases[i].DialogueID)
            {
                Phrases[i].completed = true;
                continue;
            }
            completed = false;
        }
        if(completed)
            OnComplete.Invoke();
    }
}
