using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioTaskSystem
{
    public class Task : MonoBehaviour
    {
        public List<Scenario> ParentScenario;

        public Task()
        {
            ParentScenario = new List<Scenario>();
        }

        public void Complete()
        {
            foreach(Scenario s in ParentScenario)
            {
                s.OnTaskCompleted(this);
            }
        }
        public void Fail()
        {
            Debug.Log("Task completed on " + gameObject.name);
            foreach (Scenario s in ParentScenario)
            {
                s.OnTaskFailed(this);
            }
        }

        public void AddParentScenario(Scenario s)
        {
            ParentScenario.Add(s);
        }
    }
}

