using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScenarioTaskSystem;
using static UnityEngine.Rendering.DebugUI;

public class VitalsMonitor : MonoBehaviour
{
    [Serializable]
    struct VitalValue
    {
        public string Name;
        public Text Text;
        public float Value;
        public float FluctuationRadius;
        public string OutputFormat;
        public bool Connected;
    }

    [SerializeField] VitalValue[] VitalValues;
    [SerializeField] float FluctuationIntensity;
    [SerializeField] Image AlarmImage;
    [SerializeField] float AlarmInterval;
    [SerializeField] bool FireAlarmOnStart;
    private Coroutine AlarmCoroutine;

    void Awake()
    {
        SwitchAlarm(FireAlarmOnStart);
        for (int i = 0; i < VitalValues.Length; i++)
        {
            VitalValues[i].Text.text = VitalValues[i].Value.ToString(VitalValues[i].OutputFormat);
        }
    }

    //changes a given vital value linearly
    IEnumerator ChangeVitalValue(int id, float toValue, float interval)
    {
        float initialVal = VitalValues[id].Value;
        for (float i = 0; i < 1; i += Time.deltaTime / interval)
        {
            VitalValues[id].Value = Mathf.Lerp(initialVal, toValue, i);
            VitalValues[id].Text.text = VitalValues[id].Value.ToString(VitalValues[id].OutputFormat);
            yield return 0;
        }
        VitalValues[id].Value = toValue;
        VitalValues[id].Text.text = VitalValues[id].Value.ToString();
    }

    public void ChangeValueInspector(string deserialized)
    {
        string[] args = deserialized.Split(',');
        if (args.Length != 3)
        {
            Debug.LogError("This function accepts exactly 3 arguments!");
            return;
        }
        int id;
        float toValue, interval;
        if(int.TryParse(args[0],out id) && float.TryParse(args[1], out toValue) && float.TryParse(args[2], out interval))
        {
            ChangeValue(id, toValue, interval);
        }
        else
        {
            Debug.LogError("Could not parse some of your inputs: " + deserialized);
        }
    }
    
    public void ChangeValue(int id, float toValue, float interval)
    {
        if(id < VitalValues.Length)
        {
            StartCoroutine(ChangeVitalValue(id,toValue,interval));
        }
    }

    IEnumerator AlarmFiring()
    {
        while (true)
        {
            for (float i = 0; i < 1; i += Time.deltaTime / AlarmInterval)
            {
                yield return 0;
            }
            AlarmImage.gameObject.SetActive(false);
            for (float i = 0; i < 1; i += Time.deltaTime / AlarmInterval)
            {
                yield return 0;
            }
            AlarmImage.gameObject.SetActive(true);
        }
    }

    public void SwitchAlarm(bool alarm)
    {
        if (alarm)
        {
            if(TryGetComponent<AudioSource>(out AudioSource AS))
                AS.Play();
            if (AlarmCoroutine == null)
                AlarmCoroutine = StartCoroutine(AlarmFiring());
        }
        else
        {
            if (TryGetComponent<AudioSource>(out AudioSource AS))
                AS.Stop();
            if (AlarmCoroutine != null)
            {
                StopCoroutine(AlarmCoroutine);
                AlarmCoroutine = null;
                AlarmImage.gameObject.SetActive(false);
            }
        }
    }

    public void Connect(int n)
    {
        if (VitalValues[n].Connected)
            return;
        VitalValues[n].Connected = true;
        float temp = VitalValues[n].Value;
        VitalValues[n].Value = 0;
        VitalValues[n].Text.gameObject.SetActive(true);
        StartCoroutine(ChangeVitalValue(n, temp, 5));
        for (int i = 0; i < VitalValues.Length; i++)
        {
            if (!VitalValues[i].Connected)
                return;
        }
        if(TryGetComponent<Task>(out Task t))
        {
            t.Complete();
        }
    }

    public float GetValue(int ID)
    {
        return VitalValues[ID].Value;
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Random.value < FluctuationIntensity)
        {
            for(int i = 0; i < VitalValues.Length; i++)
            {
                float fluc = Mathf.Min(VitalValues[i].FluctuationRadius, VitalValues[i].Value);
                VitalValues[i].Text.text = 
                    (VitalValues[i].Value + UnityEngine.Random.Range(-fluc, fluc)).ToString(VitalValues[i].OutputFormat); 
            }
        }
    }
}
