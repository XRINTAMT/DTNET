using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumDialogue.Demo;
using System;
using ScenarioTaskSystem;
using UnityEngine.Events;

[RequireComponent(typeof(QD_DialogueDemo))]
public class DialogueEventComplex : MonoBehaviour
{
    [Serializable]
    struct DialogueTask
    {
        public int[] DialogueID;
        public bool started;
        public bool completed;
        public bool hasAny(int _id)
        {
            foreach(int dID in DialogueID)
            {
                if(_id == dID)
                    return true;
            }
            return false;
        }
    }

    [SerializeField] DialogueTask[] Phrases;
    [SerializeField] UnityEvent OnStart;
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

            if (Phrases[i].hasAny(GetComponent<QD_DialogueDemo>().handler.currentMessageInfo.ID))
            {
                Phrases[i].completed = true;
                continue;
            }

            completed = false;
        }
        if(completed)
            OnComplete.Invoke();

        for (int i = 0; i < Phrases.Length; i++)
        {
            if (Phrases[i].hasAny(GetComponent<QD_DialogueDemo>().handler.currentMessageInfo.ID) && !Phrases[i].started)
            {
                Phrases[i].started = true;
                OnStart.Invoke();
            }
            else if (Phrases[i].hasAny(GetComponent<QD_DialogueDemo>().handler.currentMessageInfo.ID) && Phrases[i].started)
            {
                Phrases[i].started = false;
            }

          
        }
    }

    
}
