using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ScenarioTaskSystem
{
    [Serializable]
    public class Scenario
    {
        [SerializeReference] List<TaskSettings> tasks;
        [SerializeReference] public Operation OnAllCompleted;
        [SerializeField] RestartSystem Restart;
        [SerializeField] bool active;
        [SerializeField] private bool guided;
        public Scenario(TaskSettings[] ts, Operation operation)
        {
            tasks = new List<TaskSettings>();
            for (int i = 0; i < ts.Length; i++)
            {
                tasks.Add(ts[i]);
                if(ts[i].Task != null)
                {
                    ts[i].Task.AddParentScenario(this);
                }
                else
                {
                    Debug.LogWarning("There is an empty task in the scenario!");
                }
            }
            OnAllCompleted = operation;
            Restart = UnityEngine.Object.FindObjectOfType<RestartSystem>();
            Debug.Log("Is the scenario active? " + active);
        }

        public void Reconnect()
        {
            foreach (TaskSettings ts in tasks)
            {
                ts.Task.AddParentScenario(this);
            }
        }

        public void SetGuidedMode(bool enabled)
        {
            guided = enabled;
        }

        public void WrongOrder(TaskSettings completedTaskSettings)
        {
            Debug.Log("Task completion aborted! Out of order!");

            if (!guided)
            {
                if (completedTaskSettings.OnWrongOrder != null)
                    completedTaskSettings.OnWrongOrder.Execute();
            }
            else
            {
                Restart.Load();
                Debug.LogWarning("Wrong completion order! Should have loaded the save here!");
            }
        }

        int RecalculateScore()
        {
            int score = 0;
            foreach (TaskSettings task in tasks)
            {
                score += task.Score;
            }
            return score;
        }

        public void OnTaskCompleted(Task taskCompleted, int score)
        {
            TaskSettings completedTaskSettings = null;
            if(tasks == null)
            {
                Debug.Log("Tasks field is empty");
            }
            foreach (TaskSettings currentTask in tasks)
            {
                if (currentTask.Task == taskCompleted)
                {
                    completedTaskSettings = currentTask;
                    if (!currentTask.Completed)
                        break;
                }
            }
            if (completedTaskSettings == null)
            {
                Debug.LogError("Error! Task not on this scenarios task list.");
                return;
            }
            if (completedTaskSettings.Completed)
            {
                Debug.LogWarning("This task is getting completed multiple times");
                return;
            }
            if (!active && guided)
            {
                WrongOrder(completedTaskSettings);
                return;
            }
            if (completedTaskSettings.Order == null)
            {
                completedTaskSettings.Completed = true;
                Debug.Log("Task completion confirmed!");
                if (completedTaskSettings.OnCompleted != null)
                    completedTaskSettings.OnCompleted.Execute();
                allCompleted();
                completedTaskSettings.Score = score;
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
                    Debug.Log("Task completion confirmed!");
                    if (completedTaskSettings.OnCompleted != null)
                        completedTaskSettings.OnCompleted.Execute();
                    completedTaskSettings.Score = score;
                    allCompleted();
                }
                else
                {
                    WrongOrder(completedTaskSettings);
                }
            }
        }

        public void OnTaskFailed(Task taskFailed)
        {
            TaskSettings completedTaskSettings = null;
            foreach (TaskSettings currentTask in tasks)
            {
                if (currentTask.Task == taskFailed)
                {
                    completedTaskSettings = currentTask;
                }
            }
            if (completedTaskSettings == null)
            {
                Debug.LogError("Error! Task not on this scenarios task list.");
                return;
            }
            Debug.Log("Task completion confirmed!");
            if (completedTaskSettings.OnFailed != null)
                completedTaskSettings.OnFailed.Execute();
            allCompleted();
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
                OnAllCompleted.Execute(RecalculateScore());
        }

        public void Activate()
        {
            active = true;
        }
    }

    [System.Serializable]
    public class TaskSettings
    {
        public Task Task;
        public Task Order;
        //[System.NonSerialized]
        public bool Completed;
        [SerializeReference] public Operation OnCompleted;
        [SerializeReference] public Operation OnWrongOrder;
        [SerializeReference] public UniversalOperation OnFailed;
        public int Score;
    }

    [System.Serializable]
    public abstract class Operation
    {
        public Operation(Operation n = null)
        {
            Next = n;
        }
        public Operation Next;
        virtual public void Execute(int n = 0)
        {
            if (Next != null)
            {
                Next.Execute(n);
            }
        }

        public void Include(Operation n)
        {
            if (Next == null)
            {
                Next = n;
            }
            else
            {
                Next.Include(n);
            }
        }
    }
}