using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ScenarioTaskSystem;
using ScenarioSystem;
//using static UnityEngine.Rendering.DebugUI;


public class VitalsMonitor : DataSaver
{
    [Serializable]
    struct VitalValue
    {
        public string Name;
        public Text Text;
        public float Value;
        public float SetValue;
        public float FluctuationRadius;
        public string OutputFormat;
        public bool Connected;
        public GameObject Graph;
        public int SensorsLeft;
    }
    [SerializeField] VitalValue[] VitalValues;
    [SerializeField] float FluctuationIntensity;
    [SerializeField] Image AlarmImage;
    [SerializeField] float AlarmInterval;
    [SerializeField] bool FireAlarmOnStart;
    [SerializeField] TaskSpecificValues DataInterface;
    [SerializeField] UnityEvent OnAllConnected;
    private Coroutine AlarmCoroutine;
    public Action<int> conneñt;
    public Action<bool> alarm;
    public Action<int, float> changeValue;
    VitalValue[] VitalValuesSaved;
    Vector3 posHR, posSO2;
    void Start()
    {
        VitalValuesSaved = new VitalValue[VitalValues.Length];
        SwitchAlarm(FireAlarmOnStart);
        for (int i = 0; i < VitalValues.Length; i++)
        {
            if (VitalValues[i].Text != null)
            {
                VitalValues[i].Text.text = VitalValues[i].Value.ToString(VitalValues[i].OutputFormat);
            }
            if (DataInterface != null)
                DataInterface.SendDataItem(VitalValues[i].Name, VitalValues[i].Connected ? 1 : 0);
            //Debug.Log("{ \n \"name\": \"" + VitalValues[i].Name + "\",\n \"value\": " + (int)VitalValues[i].Value + "\n },");
        }

        if (VitalValues.Length > 4)
        {
            if (VitalValues[0].Graph)
                posHR = VitalValues[0].Graph.transform.position;
            if (VitalValues[4].Graph)
                posSO2 = VitalValues[4].Graph.transform.position;

        }

        conneñt += CangeDisplayMonitors;
        var _es = FindObjectsOfType<ElectrodeSocket>(true);
    }

    //changes a given vital value linearly
    /*
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
    */

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
        if (int.TryParse(args[0], out id) && float.TryParse(args[1], out toValue) && float.TryParse(args[2], out interval))
        {
            ChangeValue(id, toValue, interval);
        }
        else
        {
            Debug.LogError("Could not parse some of your inputs: " + deserialized);
        }
    }

    private void CheckElectrodes()
    {
        // Get all ElectrodeSocket objects in the scene
        ElectrodeSocket[] _electrodeSockets = FindObjectsOfType<ElectrodeSocket>();
        bool _flip = false;
        bool _noisy = false;

        // Iterate through each combination of ElectrodeSockets
        for (int i = 0; i < _electrodeSockets.Length; i++)
        {
            for (int j = i + 1; j < _electrodeSockets.Length; j++)
            {
                // Check if ElectrodeSockets have connected Pads
                if (_electrodeSockets[i].ConnectedElectrode != null && _electrodeSockets[j].ConnectedElectrode != null)
                {
                    // Access Pad properties (ID, X, Y) for the two ElectrodeSockets
                    int padID1 = _electrodeSockets[i].ConnectedElectrode.ID;
                    float padX1 = _electrodeSockets[i].x;
                    float padY1 = _electrodeSockets[i].y;

                    int padID2 = _electrodeSockets[j].ConnectedElectrode.ID;
                    float padX2 = _electrodeSockets[j].x;
                    float padY2 = _electrodeSockets[j].y;

                    // Flipping logic
                    if ((padID1 == 0 || padID1 == 2) && (padX1 > padX2) && (padID2 == 1 || padID2 == 3))
                    {
                        _flip = true;
                    }
                    else if ((padID1 == 1 || padID1 == 3) && (padX1 < padX2) && (padID2 == 0 || padID2 == 2))
                    {
                        _flip = true;
                    }
                    else if ((padID1 == 0 || padID1 == 1) && (padY1 > padY2) && (padID2 == 2 || padID2 == 3))
                    {
                        _flip = true;
                    }
                    else if ((padID1 == 2 || padID1 == 3) && (padY1 < padY2) && (padID2 == 0 || padID2 == 1))
                    {
                        _flip = true;
                    }
                }
                else
                {
                    Debug.Log($"One or both ElectrodeSockets ({_electrodeSockets[i].name}, {_electrodeSockets[j].name}) are not connected to any Pad");
                }
            }
        }

        // Noisy logic (moved outside of the loop)
        foreach (ElectrodeSocket socket in _electrodeSockets)
        {
            if (socket.ConnectedElectrode != null &&
                !((socket.x == 0 && socket.y == 0) ||
                  (socket.x == 2 && socket.y == 0) ||
                  (socket.x == 0 && socket.y == 3)||
                  (socket.x == 2 && socket.y == 3)))
            {
                Debug.Log("Noisy because of " + socket.ConnectedElectrode.ID);
                _noisy = true;
            }
        }


        GraphController _gc = FindObjectOfType<GraphController>();
        if(_gc != null)
        {
            if (_noisy)
            {
                FindObjectOfType<GraphController>().AddNoise(0);
            }
            else
            {
                FindObjectOfType<GraphController>().RemoveNoise(0);
            }
        }

        if (_flip)
        {
            VitalValues[0].Graph.transform.localScale = new Vector3(VitalValues[0].Graph.transform.localScale.x,
                VitalValues[0].Graph.transform.localScale.y, Mathf.Abs(VitalValues[0].Graph.transform.localScale.z) * -1);
        }
        else
        {
            VitalValues[0].Graph.transform.localScale = new Vector3(VitalValues[0].Graph.transform.localScale.x,
                VitalValues[0].Graph.transform.localScale.y, Mathf.Abs(VitalValues[0].Graph.transform.localScale.z));
        }
    }

    public void ChangeValue(int id, float toValue, float interval)
    {
        if(id < VitalValues.Length)
        {
            VitalValues[id].SetValue = toValue;
            Debug.Log(id);
            Debug.Log(toValue);
            if (!PhotonManager._viewerApp)
                changeValue?.Invoke(id, toValue);
           
            //StartCoroutine(ChangeVitalValue(id,toValue,interval));
        }
    }

    public int NumberOfVitalValues()
    {
        return VitalValues.Length;
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
    void PlayAlarmaSound() 
    {
        if (TryGetComponent<AudioSource>(out AudioSource AS))
            AS.Play();
    }
    public void SwitchAlarm(bool alarm)
    {
        if (!PhotonManager._viewerApp) this.alarm?.Invoke(alarm);

        if (alarm)
        {
    
            Invoke("PlayAlarmaSound", 5);

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

    public void ChangeFromSensor(string name, int value)
    {
        for(int i = 0; i < VitalValues.Length; i++)
        {
            if(name == VitalValues[i].Name)
            {
                VitalValues[i].Value = value;
                return;
            }
        }
        Debug.LogError("Value " + name + " not found on the monitor");
    }

    public void Connect(int n)
    {
        if (!PhotonManager._viewerApp) 
        {
            conneñt?.Invoke(n);
        } 

        if (VitalValues[n].Connected)
            return;
        VitalValues[n].SensorsLeft -= 1;
        if (VitalValues[n].SensorsLeft > 0)
            return;
        VitalValues[n].Connected = true;
        if (DataInterface != null)
            DataInterface.SendDataItem(VitalValues[n].Name, 1);
        float temp = VitalValues[n].Value;
        VitalValues[n].Value = 0;
        VitalValues[n].Text.gameObject.SetActive(true);
        if(VitalValues[n].Graph != null)
            VitalValues[n].Graph.SetActive(true);
        //StartCoroutine(ChangeVitalValue(n, temp, 5));
        VitalValues[n].SetValue = temp;
        for (int i = 0; i < VitalValues.Length; i++)
        {
            if (!VitalValues[i].Connected)
                return;
        }
        if(TryGetComponent<ScenarioTaskSystem.Task>(out ScenarioTaskSystem.Task t))
        {
            t.Complete();
        }

        OnAllConnected.Invoke();
       
    }

    public void Disconnect(int n)
    {
        if(n == 0 || n == 1)
        {
            VitalValues[n].Connected = false;
            VitalValues[n].SensorsLeft += 1;
            CheckElectrodes();
            VitalValues[0].Graph.SetActive(false);
        }
    }

    public float GetValue(int ID)
    {
        return VitalValues[ID].Value;
    }

    // Update is called once per frame
    void Update()
    {
    
        int toSwitchAlarm = -1;
        if(DataInterface.TryGetItem("SwitchAlarm", ref toSwitchAlarm))
        {
            SwitchAlarm(toSwitchAlarm == 1);
        }
        if (UnityEngine.Random.value < FluctuationIntensity)
        {
            for(int i = 0; i < VitalValues.Length; i++)
            {
                float fluc = Mathf.Min(VitalValues[i].FluctuationRadius, VitalValues[i].Value);
                VitalValues[i].Text.text = 
                    (VitalValues[i].Value + UnityEngine.Random.Range(-fluc, fluc)).ToString(VitalValues[i].OutputFormat); 
            }
        }
        for (int i = 0; i < VitalValues.Length; i++)
        {
            float fluc = Mathf.Min(VitalValues[i].FluctuationRadius, VitalValues[i].Value);
            if (VitalValues[i].Value < VitalValues[i].SetValue)
            {
                VitalValues[i].Value += (VitalValues[i].SetValue + (VitalValues[i].SetValue - VitalValues[i].Value) + 1) * Time.deltaTime / 4;
                if (VitalValues[i].Value > VitalValues[i].SetValue)
                    VitalValues[i].Value = VitalValues[i].SetValue;
                VitalValues[i].Text.text = VitalValues[i].Value.ToString(VitalValues[i].OutputFormat);
            }
            else if (VitalValues[i].Value > VitalValues[i].SetValue)
            {
                VitalValues[i].Value -= (VitalValues[i].SetValue + (VitalValues[i].Value - VitalValues[i].SetValue) + 1) * Time.deltaTime / 4;
                if (VitalValues[i].Value < VitalValues[i].SetValue)
                    VitalValues[i].Value = VitalValues[i].SetValue;
                VitalValues[i].Text.text = VitalValues[i].Value.ToString(VitalValues[i].OutputFormat);
            }
            else if (UnityEngine.Random.value < FluctuationIntensity){
                VitalValues[i].Text.text =
                    (VitalValues[i].Value + UnityEngine.Random.Range(-fluc, fluc)).ToString(VitalValues[i].OutputFormat);
            }            
        }
    }

    public override void Save()
    {
        VitalValues.CopyTo(VitalValuesSaved, 0);
    }

    public override void Load()
    {
        VitalValuesSaved.CopyTo(VitalValues, 0);
        foreach(VitalValue _vv in VitalValues)
        {
            if (_vv.Graph != null)
                _vv.Graph.SetActive(_vv.Connected);
            if (_vv.Text != null)
            {
                float fluc = Mathf.Min(_vv.FluctuationRadius, _vv.Value);
                _vv.Text.text =
                    (_vv.Value + UnityEngine.Random.Range(-fluc, fluc)).ToString(_vv.OutputFormat);
                _vv.Text.gameObject.SetActive(_vv.Connected);
            }
        }
    }

    public void CangeDisplayMonitors(int n) 
    {
        if (n==0 || n == 1)
        {
            CheckElectrodes();
        }
    }
}
