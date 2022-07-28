using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioTaskSystem
{
    public class Task : MonoBehaviour
    {
        public Scenario ParentScenario;
        public void Complete()
        {
            ParentScenario.OnTaskCompleted(this);
        }
    }
}

