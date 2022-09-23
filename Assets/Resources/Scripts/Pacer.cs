using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class Pacer : MonoBehaviour
{
    [SerializeField] PatientData Patient;
    [SerializeField] VitalsMonitor Monitor;
    [SerializeField] Knob Output;
    [SerializeField] Knob Pace;
    [SerializeField] float[] AimVitals;
    [SerializeField] float AimPace;
    float AimOutput;
    float[] Vitals;
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
        AimOutput = ((int)Patient.weight / 10) * 10;
        maxOutputDiff = Mathf.Max(Output.maxValue - AimOutput, AimOutput - Output.minValue);
        maxPaceDiff = Mathf.Max(Pace.maxValue - AimPace, AimPace - Pace.minValue);
        Vitals = new float[Monitor.NumberOfVitalValues()];
        for(int i = 0; i < Vitals.Length; i++)
        {
            Vitals[i] = Monitor.GetValue(i);
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
        for (int i = 0; i < AimVitals.Length; i++)
        {
            float score = 1;
            score *= 1 - (Mathf.Abs(AimPace - paceValue) / maxPaceDiff);
            score *= 1 - (Mathf.Abs(AimOutput - outputValue) / maxOutputDiff);
            Monitor.ChangeValue(i, Mathf.Lerp(Vitals[i], AimVitals[i], score), 1);
            Debug.Log(score);
            if(score > 0.99)
            {
                if(TryGetComponent<Task>(out Task task))
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
