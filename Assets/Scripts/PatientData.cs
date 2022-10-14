using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScenarioSystem;

public class PatientData : MonoBehaviour
{
    [field: SerializeField] public float weight { private set; get; }

    [SerializeField] Dictionary<string, int> VitalValues;
    [SerializeField] TaskSpecificValues DataInterface;
    [SerializeField] Dictionary<string, Sensor> SubscriberSensors;

    void Awake()
    {
        for (int i = 0; i < VitalValues.Count; i++)
        {
            VitalValues = DataInterface.GetDataItem();
        }
    }

    //changes a given vital value linearly
    IEnumerator ChangeVitalValue(string id, int toValue, float interval)
    {
        int initialVal = VitalValues[id];
        for (float i = 0; i < 1; i += Time.deltaTime / interval)
        {
            VitalValues[id] = (int)Mathf.Lerp(initialVal, toValue, i);
            SubscriberSensors[id].SendData(id, VitalValues[id]);
            yield return 0;
        }
        VitalValues[id] = toValue;
        SubscriberSensors[id].SendData(id, VitalValues[id]);
    }

    public void ChangeValue(string id, int toValue, float interval)
    {
        if (VitalValues.ContainsKey(id))
        {
            StartCoroutine(ChangeVitalValue(id, toValue, interval));
        }
    }

    public int NumberOfVitalValues()
    {
        return VitalValues.Count;
    }

    public float GetValue(string ID)
    {
        return VitalValues[ID];
    }

    public void Subscribe(Sensor Subscriber)
    {
        for (int i = 0; i < Subscriber.ValuesScanned.Length; i++)
        {
            if (VitalValues.ContainsKey(Subscriber.ValuesScanned[i]))
            {
                SubscriberSensors[Subscriber.ValuesScanned[i]] = Subscriber;
            }
            else
            {
                Debug.LogError("Value " + Subscriber.ValuesScanned[i] + " does not exist");
            }
        }
        Subscriber.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(string val in VitalValues.Keys)
        {
            int temp = 0;
            if (DataInterface.TryGetItem(val, ref temp))
            {
                VitalValues[val] = temp;
                SubscriberSensors[val].SendData(val, VitalValues[val]);
            }
        }
    }
}
