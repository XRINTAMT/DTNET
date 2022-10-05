using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class ScenarioBehaviour : MonoBehaviour
    {
        [SerializeField] string[] serializedConditions;
        [SerializeField] string[] conditionTypes;
        [SerializeField] Task[] Tasks;
        //[SerializeField] InjectionChecker IJ;

        public void Awake()
        {
            
            for (int i = 0; i < Tasks.Length; i++)
            {
                
            }
            
            //Debug.Log(JsonUtility.ToJson(IJ));
        }

        public void Update()
        {
            foreach(Task task in Tasks)
            {
                if (!task.Completed)
                {
                    
                }
            }
        }
    }
}
