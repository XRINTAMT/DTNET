using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceRow : MonoBehaviour
{
    [SerializeField] MultipleChoiceBehaviour choice;
    [SerializeField] GameObject[] Ticks;
    [SerializeField] Text OxygenPerMinute;
    [SerializeField] Text EscalationOfCare;
    [SerializeField] Observation obs;
    public Action <MultipleChoiceRow> submit;


    public void RenderObservation(Observation _o)
    {
        obs = new Observation(_o.values, _o.wrong);
        for (int i = 0; i <= 2; i++)
        {
            PlaceTick(i, _o.values[i]);
        }
        if (_o.values[3] == 0)
        {
            OxygenPerMinute.gameObject.SetActive(false);
            Ticks[3].SetActive(true);
        }
        else
        {
            Ticks[3].SetActive(false);
            OxygenPerMinute.gameObject.SetActive(true);
            OxygenPerMinute.text = _o.values[3].ToString();
        }
        if (_o.values[4] <= 5)
        {
            Ticks[5].SetActive(false);
            PlaceTick(4, _o.values[4]);
        }
        else
        {
            Ticks[4].SetActive(false);
            PlaceTick(5, _o.values[4]-6);
        }
        for (int i = 5; i < _o.values.Length-1; i++)
        {
            PlaceTick(i+1, _o.values[i]);
        }
        EscalationOfCare.text = (_o.values[_o.values.Length - 1] == 0) ? "N" : "Y";
    }

    private void PlaceTick(int _tickId, int _position)
    {
        Ticks[_tickId].GetComponent<RectTransform>().localPosition = new Vector3(Ticks[_tickId].GetComponent<SaveTick>().LocalPosition.x, Ticks[_tickId].GetComponent<SaveTick>().LocalPosition.y - (_position * 27.25f), Ticks[_tickId].GetComponent<SaveTick>().LocalPosition.z);
    }

    public void Submit()
    {
        submit?.Invoke(this);
        choice.ChooseAnswer(obs);
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
