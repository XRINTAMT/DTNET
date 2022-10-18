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
    [SerializeField] Dictionary<string, int> AimVitals;
    [SerializeField] float AimPace;
    [SerializeField] Sensor Pad;
    PatientData Patient;
    float AimOutput;
    Dictionary<string, int> Vitals;
    float maxOutputDiff;
    float maxPaceDiff;
    float paceValue;
    float outputValue;
    bool pacing = false;

    void Start()
    {

    }

    public void StartPacing()
    {
        Patient = Pad.patient;
        AimOutput = ((int)Monitor.GetValue(5) / 10) * 10;
        AimVitals = DataInterface.GetDataItem();
        maxOutputDiff = Mathf.Max(Output.maxValue - AimOutput, AimOutput - Output.minValue);
        maxPaceDiff = Mathf.Max(Pace.maxValue - AimPace, AimPace - Pace.minValue);
        Vitals = new Dictionary<string,int>();
        foreach(string key in Patient.Values())
        {
            Vitals[key] = Patient.GetValue(key);
        }
        pacing = true;
    }

    public void OnValueChanged(int id, float value)
    {
        if (!pacing)
            return;
        if (id == 0)
        {
            outputValue = value;
        }
        else
        {
            paceValue = value;
        }
        foreach (string key in Patient.Values())
        {
            float score = 1;
            score *= 1 - (Mathf.Abs(AimPace - paceValue) / maxPaceDiff);
            score *= 1 - (Mathf.Abs(AimOutput - outputValue) / maxOutputDiff);
            Patient.ChangeValue(key, (int)Mathf.Lerp(Vitals[key], AimVitals[key], score), 1);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
