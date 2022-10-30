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
        [SerializeField] float RefreshTime;
        float Refresh;
        

        public void Awake()
        {
            Refresh = 0;
            ScenarioInfo = JsonUtility.FromJson<Room>(ScenarioJSON);
            Transform playerTransform = Object.FindObjectOfType<PlayerObject>().transform;
            playerTransform.position = new Vector3(ScenarioInfo.PlayerX, ScenarioInfo.PlayerY, ScenarioInfo.PlayerZ);
            playerTransform.rotation = Quaternion.Euler(0, ScenarioInfo.PlayerRot, 0);
            Objects = new GameObject[ScenarioInfo.Objects.Length];
            foreach (Obj item in ScenarioInfo.Objects)
            {
                GameObject Temp = Instantiate(Resources.Load("Prefabs/Constructor/"+item.type)) as GameObject;
                if (Temp == null)
                {
                    Debug.LogError("Prefabs/ConstructorItems/" + item.type + " does not exist");
                }
                else
                {
                    Temp.transform.position = new Vector3(item.x, item.y, item.z);
                    Temp.transform.rotation = Quaternion.Euler(Temp.transform.rotation.eulerAngles.x, item.rot, Temp.transform.rotation.eulerAngles.z);
                    TaskSpecificValues Values = Temp.GetComponent<TaskSpecificValues>();
                    if (Values == null)
                    {
                        Debug.LogError(item.type + " does not have TaskSpecificValues");
                    }
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
                //Debug.Log(task.OnComplete);
                foreach (Command command in task.OnComplete)
                {
                    command.ConnectToObjectsBase(this);
                }
            }
            //Debug.Log(JsonUtility.ToJson(IJ));
        }

        public void Update()
        {
            Refresh += Time.deltaTime;
            if(Refresh > RefreshTime)
            {
                Refresh -= RefreshTime;
                GoThroughTheScenario();
            }
            /*
            foreach(Task task in Tasks)
            {
                if (!task.Completed)
                {
                    
                }
            }
            */
        }

        private void GoThroughTheScenario()
        {
            for (int i = 0; i < ScenarioInfo.Tasks.Length; i++)
            {
                if (ScenarioInfo.Tasks[i].Completed)
                    continue;
                bool goOn = false;
                if (ScenarioInfo.Tasks[i].Completed)
                {
                    goOn = true;
                }
                else
                {
                    //Debug.Log("Checking " + ScenarioInfo.Tasks[i].name);
                    if (GoThroughTaskConditions(ScenarioInfo.Tasks[i]))
                    {
                        goOn = true;
                        //Debug.Log(ScenarioInfo.Tasks[i].name + " completed");
                        foreach (Command command in ScenarioInfo.Tasks[i].OnComplete)
                        {
                            command.Completed();
                        }
                        ScenarioInfo.Tasks[i].Completed = true;
                    }
                    else
                    {
                        Debug.Log(ScenarioInfo.Tasks[i].name + " failed");
                    }

                }
                if (!goOn)
                {
                    if (i + 1 < ScenarioInfo.Tasks.Length)
                    {
                        if (ScenarioInfo.Tasks[i + 1].WithPrevious)
                        {
                            goOn = true;
                        }
                    }
                }
                if (!goOn)
                {
                    return;
                }
            }
        }

        private bool GoThroughTaskConditions(Task task)
        {
            for (int i = 0; i < task.Conditions.Length; i++)
            {
                if (!task.Conditions[i].Check())
                {
                    return false;
                }
            }
            return true;
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
