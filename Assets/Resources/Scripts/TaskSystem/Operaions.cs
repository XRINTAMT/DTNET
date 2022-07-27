using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace ScenarioTaskSystem
{
    public class TaskCompleter : Operation
    {
        private Task task;

        public TaskCompleter(Task t, Operation n = null) : base(n)
        {
            task = t;
        }

        override public void Execute()
        {
            task.ParentScenario.OnTaskCompleted(task);
            base.Execute();
        }
    }

    [System.Serializable]
    public class RoomChanger : Operation
    {
        [SerializeField] int RoomID = -1;

        override public void Execute()
        {
            if (RoomID == -1)
            {
                Debug.LogError("RoomID not set for task completion action");
            }
            else
            {
                SceneManager.LoadScene(RoomID);
            }
            base.Execute();
        }
    }

    [System.Serializable]
    public class UniversalOperation : Operation
    {
        [SerializeField] UnityEvent Function = null;

        override public void Execute()
        {
            if (Function == null)
            {
                Debug.LogError("Event not set for task completion action");
            }
            else
            {
                Function.Invoke();
            }
            base.Execute();
        }
    }
}
