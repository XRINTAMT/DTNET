using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioTaskSystem
{
    public class Task : MonoBehaviour
    {
        [SerializeReference] public List<Scenario> ParentScenario;

        public Task()
        {
            ParentScenario = new List<Scenario>();
        }

        public void Complete(int score)
        {
            foreach(Scenario s in ParentScenario)
            {
                Debug.Log("Task completed on " + name + " with the score of " + score);
                s.OnTaskCompleted(this, score);
            }
        }

        public void Complete()
        {
            int score = 0;
            Debug.Log("Task completed on " + name + " with the score of " + score);
            foreach (Scenario s in ParentScenario)
            {
                s.OnTaskCompleted(this, score);
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

        public void AddParentScenario(in Scenario s)
        {
            ParentScenario.Clear();
            ParentScenario.Add(s);
            //That's what we probably want
            //ParentScenario.Add(s);
        }
    }
}

