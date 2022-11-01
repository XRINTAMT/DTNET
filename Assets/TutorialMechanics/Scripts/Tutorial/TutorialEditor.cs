using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEditor : MonoBehaviour
{
    public List<TutorialTask> ListTasks = new List<TutorialTask>();

    public UnityEvent OnTutorialStart = new UnityEvent();
    public UnityEvent OnTutorialComplete = new UnityEvent();


    private void Start()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        StartTask(0);
        OnTutorialStart?.Invoke();
    }


    public void LocomtionSystem(bool teleport) 
    {
        if (!teleport) AutoHandPlayer.teleportMove = false;
       
        if (teleport) AutoHandPlayer.teleportMove = true;
       
    }
    public void StartTask(int index)
    {
        if (index >= 0 && index < ListTasks.Count)
        {
            StartCoroutine(StartTask_Coroutine(index));
        }
    }
    
    public void CompleteTask(int index)
    {
        if (index >= 0 && index < ListTasks.Count)
        {
            ListTasks[index].CompleteTask();

            if (index == (ListTasks.Count - 1) && ListTasks[index].IsComplete)
            {
                OnTutorialComplete?.Invoke();
            }
        }
    }

    public void CancelTask(int index)
    {
        if (index >= 0 && index < ListTasks.Count)
        {
            ListTasks[index].CancelTask();
        }
    }

    private IEnumerator StartTask_Coroutine(int index)
    {
        yield return new WaitForSeconds(ListTasks[index].Delay);
        ListTasks[index].StartTask();
    }
}

[Serializable]
public class TutorialTask
{
    [Min(0f)]
    [Tooltip("Через сколько запустить задачу с момента вызова StartTask()")]
    public float Delay = 0f;

    public UnityEvent OnTaskStarted = new UnityEvent();
    public UnityEvent OnTaskCompleted = new UnityEvent();
    public UnityEvent OnTaskCanceled = new UnityEvent();

    private bool _isStart = false;
    private bool _isComplete = false;

    public bool IsStart
    {
        get
        {
            return _isStart;
        }
    }
    public bool IsComplete
    {
        get
        {
            return _isComplete;
        }
    }

    public void StartTask()
    {
        if (!_isStart && !_isComplete)
        {
            _isStart = true;
            _isComplete = false;
            OnTaskStarted?.Invoke();
        }
    }

    public void CompleteTask()
    {
        if (_isStart && !_isComplete)
        {
            _isStart = false;
            _isComplete = true;
            OnTaskCompleted?.Invoke();
        }        
    }

    public void CancelTask()
    {
        if (_isStart && !_isComplete)
        {
            _isStart = false;
            _isComplete = false;
            OnTaskCanceled?.Invoke();
        }
    }
}
