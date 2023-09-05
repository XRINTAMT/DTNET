using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class MultipleChoiceBehaviour : DataSaver
{
    [SerializeField] int[] correctValues;
    [SerializeField] MultipleChoiceRow[] options;
    [SerializeField] MultipleChoiceRow renderResult;
    [SerializeField] Date Datestamp;
    [SerializeField] Date Timestamp;
    [SerializeField] GameObject MultupleChoiceUI;
    [SerializeField] GameObject Next;

    void OnEnable()
    {
        Regenerate();
    }

    private void Regenerate()
    {
        int correctRow = (int)Mathf.Floor(Random.Range(0, options.Length));
        for (int i = 0; i < options.Length; i++)
        {
            options[i].RenderObservation(new Observation(correctValues, (i != correctRow)));
        }
    }

    public void ChooseAnswer(Observation _o)
    {
        renderResult.gameObject.SetActive(true);
        renderResult.RenderObservation(_o);
        Datestamp.GenerateDatestamp();
        Timestamp.GenerateTimeStamp();
        MultupleChoiceUI.SetActive(false);
        if(Next != null)
            Next.SetActive(true);
        GetComponent<Task>().Complete((_o.wrong) ? 0 : 1);
    }

    void Update()
    {
        
    }

    public override void Save()
    {
        
    }

    public override void Load()
    {
        if (gameObject.activeInHierarchy)
        {
            Regenerate();
        }
    }
}

[System.Serializable]
public class Observation
{
    public int Respitations;
    public int Scale1;
    public int Scale2;
    public int Oxygen;
    public int BloodPressure;
    public int Pulse;
    public int Consciousness;
    public int Temperature;
    public bool wrong;
    public int[] values;
    static int[] valueLimits = { 6, 3, 7, 3, 13, 12, 4, 5, 2};

    /*
    public Observation(int _r, int _s1, int _s2, int _o, int _bp, int _p, int _c, int _t, bool _isWrong)
    {

    }
    */

    public Observation(int[] _values, bool _isWrong)
    {
        values = (int[])_values.Clone();
        wrong = false;
        //randomize the values for the wrong one
        if (_isWrong)
        {
            wrong = true;
            int r1, r2;
            r1 = (int)Mathf.Floor(Random.Range(0, _values.Length));
            do
            {
                r2 = (int)Mathf.Floor(Random.Range(0, _values.Length));
            }
            while (r1 == r2);
            do
            {
                values[r1] = (int)Mathf.Round(Random.Range(0, valueLimits[r1]));
            }
            while (values[r1] == _values[r1]);
            do
            {
                values[r2] = (int)Mathf.Round(Random.Range(0, valueLimits[r2]));
            }
            while (values[r2] == _values[r2]);
        }
    }
}