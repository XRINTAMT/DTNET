using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;
using UnityEngine.UI;

public class ObservationSheet : MonoBehaviour
{
    [SerializeField] GameObject NextRow;
    [SerializeField] string[] RightValues;
    string[] values;
    Coroutine countdown = null;
    static float TimeOut = 60;
    public GameObject arrowGuide;
    public bool test;
    void Awake()
    {
        values = new string[RightValues.Length];
        for (int i = 0; i < RightValues.Length; i++)
        {
            values[i] = "Empty";
        }
    }

    public void ChangeValueInspector(string input)
    {
        string[] args = input.Split(',');
        if (args.Length != 2)
        {
            Debug.LogError("This function accepts exactly 2 arguments!");
            return;
        }
        int id;
        if (int.TryParse(args[0], out id))
        {
            ChangeValue(id, args[1]);
        }
        else
        {
            Debug.LogError("Could not parse some of your inputs: " + input);
        }
    }

    public void ChangeValue(int id, string val)
    {
        values[id] = val;
    }

    public void Submit()
    {
        bool correct = true;
        for (int i = 0; i < RightValues.Length; i++)
        {
            if (values[i] != RightValues[i])
            {
                correct = false;
                Debug.Log(values[i] + " is not the same as " + RightValues[i]);
                break;
            }
        }
        if (correct)
        {
            if (TryGetComponent<Task>(out Task a))
                a.Complete(1);
        }
        else
        {
            if (TryGetComponent<Task>(out Task a))
                a.Complete(0);
        }
        BakeAndContinue();
    }

    public void BakeAndContinue() {
        Toggle[] tickboxes = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < tickboxes.Length; i++) {
            if (tickboxes[i].isOn) {
                tickboxes[i].graphic.transform.SetParent(transform.parent);
            }
        }
        Text[] fields = GetComponentsInChildren<Text>();
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].color.a >= 0.9f)
            {
                fields[i].transform.SetParent(transform.parent);
            }
        }
        if (NextRow != null)
        {
            NextRow.SetActive(true);
            if (PlayerPrefs.GetInt("GuidedMode")==1)
            {
                GetComponent<ObservationSheet>().arrowGuide.SetActive(false);
                NextRow.GetComponent<ObservationSheet>().arrowGuide.SetActive(true);
            }
        }
        /*
        InputField[] fields = GetComponentsInChildren<InputField>();
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].textComponent.transform.SetParent(transform.parent);
        }
        if (NextRow != null)
        {
            NextRow.SetActive(true);
        }
        */
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StartCoundown()
    {
        countdown = StartCoroutine(Count(TimeOut));
    }
    public void AbortCoundown()
    {
        if(countdown != null)
        {
            StopCoroutine(countdown);
            countdown = null;
        }
    }

    IEnumerator Count(float timeSeconds)
    {
        for(float i = 0; i < timeSeconds; i += Time.deltaTime)
        {
            yield return 0;
        }
        if (TryGetComponent<Task>(out Task a))
            a.Complete(0);
        BakeAndContinue();
    }

    void Update()
    {
        if (test) 
        {
            BakeAndContinue();
            test = false;
        }
    }
}
