using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class TaskSpecificValues : MonoBehaviour
    {
        public Dictionary<string, int> Values;
        public Dictionary<string, int> Changes;

        private void Start()
        {
            Values = new Dictionary<string, int>();
        }

        public void SendDataItem(string name, int val)
        {
            Values[name] = val;
        }

        public void SendDataSystem(string name, int val)
        {
            Changes[name] = val;
        }

        public Dictionary<string, int> GetDataItem()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>(Changes);
            Changes.Clear();
            return temp;
        }

        public Dictionary<string, int> GetDataSystem()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>(Values);
            Values.Clear();
            return temp;
        }
    }
}
