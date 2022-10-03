using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace ScenarioSystem
{
    [Serializable]
    public class Task
    {
        [SerializeField] string name;
        public bool WithPrevious;
        public bool Completed;
        public ConditionChecker Condition;
        public UnityEvent OnCompleted;
        //public UnityEvent OnOutOfOrder;

    }

    public interface ConditionChecker
    {
        abstract public int Check();
    }

    [Serializable]
    public class InjectionChecker : ConditionChecker
    {
        [Serializable]
        struct Dose
        {
            public string Ingredient;
            public float Amount;
            public float Error;
        }
        [SerializeField] Dose[] doses;

        private Syringe[] Syringes;

        public InjectionChecker()
        {
            Syringes = UnityEngine.Object.FindObjectsOfType<Syringe>();
        }

        public int Check()
        {
            throw new NotImplementedException();
        }
    }
}

