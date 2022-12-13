using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;
using ScenarioSystem;

public class Pacer : MonoBehaviour
{
    [System.Serializable]
    class Milestone
    {
        public int Value;
        public UnityEngine.Events.UnityEvent OnReached;
    }

    [SerializeField] Milestone[] Milestones;
    [SerializeField] TaskSpecificValues DataInterface;
    [SerializeField] VitalsMonitor Monitor;
    [SerializeField] Knob Output;
    [SerializeField] Knob Pace;
    [SerializeField] float AimPace;
    [SerializeField] Sensor Pad;
    [SerializeField] float AimOutput;

    
    float maxOutputDiff;
    float maxPaceDiff;
    float paceValue;
    float outputValue;
    bool pacing = false;
    int currentMilestone = 0;
    Coroutine DelayedMilestone;

    void Awake()
    {
        paceValue = 60;
        outputValue = 0;
        DataInterface.SendDataItem("Output", (int)outputValue);
        DataInterface.SendDataItem("Pace", (int)paceValue);

    }

    public void StartPacing()
    {
        //AimOutput = ((int)Monitor.GetValue(5) / 10) * 10;
        maxOutputDiff = Mathf.Max(Output.maxValue - AimOutput, AimOutput - Output.minValue);
        maxPaceDiff = Mathf.Max(Pace.maxValue - AimPace, AimPace - Pace.minValue);
        pacing = true;
    }

    public void OnValueChanged(int id, float value)
    {
        if (!pacing)
            return;
        if (id == 0)
        {
            outputValue = value;
            DataInterface.SendDataItem("Output", (int)value);
            CheckOutputMilestone();
        }
        else
        {
            paceValue = value;
            DataInterface.SendDataItem("Pace", (int)value);
        }
        float score = 1;
        score *= 1 - (Mathf.Abs(AimPace - paceValue) / maxPaceDiff);
        score *= 1 - (Mathf.Abs(AimOutput - outputValue) / maxOutputDiff);
        Debug.Log(score);
        if (score == 1 && currentMilestone == Milestones.Length)
        {
            Debug.Log("PatientCured");
            if (TryGetComponent<ScenarioTaskSystem.Task>(out ScenarioTaskSystem.Task task))
            {
                task.Complete(1);
            }
        }
    }

    private void CheckOutputMilestone()
    {
        if(DelayedMilestone == null)
        {
            if(currentMilestone < Milestones.Length)
            {
                if (Mathf.Abs(outputValue - Milestones[currentMilestone].Value) < 5)
                {
                    DelayedMilestone = StartCoroutine(IntermilestonePause());
                    Debug.Log("Milestone " + Milestones[currentMilestone].Value + " reached");
                    Milestones[currentMilestone].OnReached.Invoke();
                    currentMilestone++;
                }
            }
        }
    }

    IEnumerator IntermilestonePause()
    {
        for(float i = 0; i < 5; i += Time.deltaTime)
        {
            yield return 0;
        }
        DelayedMilestone = null;
        CheckOutputMilestone();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
