using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class TaskSpecificValues : MonoBehaviour
    {
        Dictionary<string, int> Values;
        Dictionary<string, int> Changes;

        private void Awake()
        {
            Values = new Dictionary<string, int>();
            Changes = new Dictionary<string, int>();
        }

        public void SendDataItem(string name, int val)
        {
            Values[name] = val;
            //Debug.Log(name + ": " + val);
        }

        public void SendDataSystem(string name, int val)
        {
            Changes[name] = val;
        }

        public bool TryGetItem(string name, ref int val)
        {
            if (Changes.ContainsKey(name))
            {
                val = Changes[name];
                Changes.Remove(name);
                return true;
            }
            return false;
        }

        public bool TryGetSystem(string name, ref int val)
        {
            if (Values.ContainsKey(name))
            {
                val = Values[name];
                return true;
            }
            return false;
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
            return temp;
        }

        public void Update()
        {
            foreach (string key in Values.Keys)
            {
                //Debug.Log(gameObject.name + ": " + key + " " + Values[key]);
            }
            
        }
    }
}
