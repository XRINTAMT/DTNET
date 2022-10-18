using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;
using ScenarioSystem;

public class Pacer : MonoBehaviour
{
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

    void Start()
    {
        DataInterface.SendDataItem("Output", 0);
        DataInterface.SendDataItem("Pace", 60);
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
        if (score == 1)
        {
            Debug.Log("PatientCured");
            if (TryGetComponent<ScenarioTaskSystem.Task>(out ScenarioTaskSystem.Task task))
            {
                task.Complete();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
