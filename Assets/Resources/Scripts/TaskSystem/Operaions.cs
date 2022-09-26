using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace ScenarioTaskSystem
{
    [System.Serializable]
    public class TaskCompleter : Operation
    {
        private Task task;

        public TaskCompleter(Task t, Operation n = null) : base(n)
        {
            if (t == null)
            {
                Debug.LogError("You are creating a task completer with null task in it!");
            }
            task = t;
        }

        override public void Execute()
        {
            if (task == null)
            {
                Debug.LogError("Cannot use a task completer with a null task");
            }
            task.Complete();
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
                Debug.LogWarning("RoomID not set for task completion action!");
            }
            else
            {
                SceneManager.LoadScene(RoomID);
            }
            base.Execute();
        }
    }

    public class ScenarioActivator : Operation
    {
        Scenario toActivate;

        public ScenarioActivator(Scenario s, Operation n = null) : base(n)
        {
            if (s == null)
            {
                Debug.LogError("You are creating a task completer with null task in it!");
            }
            toActivate = s;
        }

        override public void Execute()
        {
            toActivate.Activate();
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
