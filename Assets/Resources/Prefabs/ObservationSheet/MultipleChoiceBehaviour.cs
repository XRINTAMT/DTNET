using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class MultipleChoiceBehaviour : MonoBehaviour
{
    [SerializeField] int[] correctValues;
    [SerializeField] MultipleChoiceRow[] options;
    [SerializeField] MultipleChoiceRow renderResult;
    [SerializeField] Date Datestamp;
    [SerializeField] Date Timestamp;
    [SerializeField] GameObject MultupleChoiceUI;
    [SerializeField] GameObject Next;

    void Awake()
    {
        GameObject ChangableItemsContainer = findParentWithTag("ChangableItems");
        if (!ChangableItemsContainer.name.Contains("(Clone)"))
        {
            int correctRow = (int)Mathf.Floor(Random.Range(0, options.Length));
            for (int i = 0; i < options.Length; i++)
            {
                options[i].RenderObservation(new Observation(correctValues, (i != correctRow)));
            }
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

    private GameObject findParentWithTag(string _tag)
    {
        var parent = gameObject.transform.parent;
        while(parent != null) {
            if (parent.tag == _tag)
            {
                return parent.gameObject as GameObject;
            }
            parent = parent.transform.parent;
        }
        return null;
    }
}

[System.Serializable]
public class Observation
{
    int Respitations;
    int Scale1;
    int Scale2;
    int Oxygen;
    int BloodPressure;
    int Pulse;
    int Consciousness;
    int Temperature;
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