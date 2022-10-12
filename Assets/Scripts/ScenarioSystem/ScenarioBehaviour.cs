using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem
{
    public class ScenarioBehaviour : MonoBehaviour
    {
        [SerializeField] string ScenarioJSON;
        GameObject[] Objects;
        [SerializeField]
        Room ScenarioInfo;

        public void Awake()
        {
            ScenarioInfo = JsonUtility.FromJson<Room>(ScenarioJSON);

            Transform playerTransform = Object.FindObjectOfType<PlayerObject>().transform;
            playerTransform.position = new Vector3(ScenarioInfo.PlayerX, ScenarioInfo.PlayerY, ScenarioInfo.PlayerZ);
            playerTransform.rotation = Quaternion.Euler(0, ScenarioInfo.PlayerRot, 0);
            Objects = new GameObject[ScenarioInfo.Objects.Length];
            foreach (Obj item in ScenarioInfo.Objects)
            {
                GameObject Temp = Instantiate(Resources.Load("Prefabs/ConstructorItems/"+item.type)) as GameObject;
                if (Temp == null)
                {
                    Debug.Log("Prefabs/ConstructorItems/" + item.type + " does not exist");
                }
                else
                {
                    Temp.transform.position = new Vector3(item.x, item.y, item.z);
                    Temp.transform.rotation = Quaternion.Euler(Temp.transform.rotation.eulerAngles.x, item.rot, Temp.transform.rotation.eulerAngles.z);
                    TaskSpecificValues Values = Temp.GetComponent<TaskSpecificValues>();
                    if(item.ObjectSpecificValues != null)
                    {
                        foreach (CustomField field in item.ObjectSpecificValues)
                        {
                            Values.SendDataSystem(field.name, field.value);
                        }
                    }
                    Objects[item.id] = Temp;
                }
            }
            foreach (Task task in ScenarioInfo.Tasks)
            {
                foreach(ConditionChecker checker in task.Conditions)
                {
                    checker.ConnectToObjectsBase(this);
                }
            }
            //Debug.Log(JsonUtility.ToJson(IJ));
        }

        public void Update()
        {
            /*
            foreach(Task task in Tasks)
            {
                if (!task.Completed)
                {
                    
                }
            }
            */
        }

        public TaskSpecificValues AccessValues(int id)
        {
            if (Objects[id].TryGetComponent<TaskSpecificValues>(out TaskSpecificValues result))
            {
                return result;
            }
            else
            {
                Debug.LogWarning(Objects[id].name + " does not have a TaskSpecificValues component on it, you cannot use it as a subject of a task.");
                return null;
            }
        }
    }
}
