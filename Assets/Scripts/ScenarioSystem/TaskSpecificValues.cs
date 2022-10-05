using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class TaskSpecificValues : MonoBehaviour
    {
        public Dictionary<string, int> Values;

        private void Start()
        {
            Values = new Dictionary<string, int>();
        }
    }
}
