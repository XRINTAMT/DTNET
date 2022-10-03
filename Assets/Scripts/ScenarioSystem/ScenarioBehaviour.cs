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
                switch (conditionTypes[i]){
                    case ("Injection"):
                        Tasks[i].Condition = JsonUtility.FromJson<InjectionChecker>(serializedConditions[i]);
                        break;
                }
                
            }
            
            //Debug.Log(JsonUtility.ToJson(IJ));
        }

        public void Update()
        {
            foreach(Task task in Tasks)
            {
                if (!task.Completed)
                {
                    task.Condition.Check();
                }
            }
        }
    }
}
