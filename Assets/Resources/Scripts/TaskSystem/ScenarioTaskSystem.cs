using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioTaskSystem
{
    public class Scenario
    {
        List<TaskSettings> tasks;
        public Operation OnAllCompleted;

        public Scenario(TaskSettings[] ts, Operation operation)
        {
            tasks = new List<TaskSettings>();
            for(int i = 0; i < ts.Length; i++)
            {
                tasks.Add(ts[i]);
                ts[i].Task.ParentScenario = this;
            }
            OnAllCompleted = operation;
        }

        public void OnTaskCompleted(Task taskCompleted)
        {
            TaskSettings completedTaskSettings = null;
            foreach(TaskSettings currentTask in tasks)
            {
               if(currentTask.Task == taskCompleted)
                {
                    completedTaskSettings = currentTask;
                    if(!currentTask.Completed)
                        break;
                }
            }
            if(completedTaskSettings == null)
            {
                Debug.LogError("Error! Task not on this scenarios task list.");
                return;
            }
            if (completedTaskSettings.Completed)
            {
                Debug.LogWarning("This task is getting completed multiple times");
                return;
            }
            if (completedTaskSettings.Order == null)
            {
                completedTaskSettings.Completed = true;
                if (completedTaskSettings.OnCompleted != null)
                    completedTaskSettings.OnCompleted.Execute();
                allCompleted();
            }
            else
            {
                bool ordered = true;
                foreach (TaskSettings currentTask in tasks)
                {
                    if (currentTask == completedTaskSettings)
                    {
                        break;
                    }
                    if (currentTask.Completed == false)
                    {
                        ordered = false;
                        break;
                    }
                }
                if (ordered)
                {
                    completedTaskSettings.Completed = true;
                    if (completedTaskSettings.OnCompleted != null)
                        completedTaskSettings.OnCompleted.Execute();
                    allCompleted();
                }
                else
                {
                    if (completedTaskSettings.OnWrongOrder != null)
                        completedTaskSettings.OnWrongOrder.Execute();
                }
            }
        }

        private void allCompleted()
        {
            foreach (TaskSettings currentTask in tasks)
            {
                if (!currentTask.Completed)
                {
                    return;
                }
            }
            if (OnAllCompleted != null)
                OnAllCompleted.Execute();
        }
    }

    [System.Serializable]
    public class TaskSettings
    {
        public Task Task;
        public Task Order;
        //[System.NonSerialized]
        public bool Completed;
        public Operation OnCompleted;
        public Operation OnWrongOrder;
    }

    [System.Serializable]
    public abstract class Operation
    {
        public Operation(Operation n = null)
        {
            Next = n;
        }
        public Operation Next;
        virtual public void Execute()
        {
            if(Next != null)
            {
                Next.Execute();
            }
        }
    }
}