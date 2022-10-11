using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class TaskSpecificValues : MonoBehaviour
    {
        public Dictionary<string, int> Values;
        public Dictionary<string, bool> Pull;

        private void Start()
        {
            Values = new Dictionary<string, int>();
        }

        private void ChangeValues(string name, int val)
        {
            Values[name] = val;
            Pull[name] = true;
        }
    }
}
