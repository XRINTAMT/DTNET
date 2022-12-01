using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PerfusionPumpSettings : MonoBehaviour
{
    [Serializable]
    struct Digit
    {
        public float multiplier;
        public PerfusionPumpNumber number;
    }

    [SerializeField] Digit[] digits;

    int selector;
    float Value;

    void Start()
    {
        selector = digits.Length - 1;
        digits[selector].number.Select(true);
    }

    public void Up()
    {
        digits[selector].number.Up();
    }

    public void Down()
    {
        digits[selector].number.Down();
    }

    public float GetValue()
    {
        Value = 0;
        for(int i = 0; i < digits.Length; i++)
        {
            Value += digits[i].number.Value * digits[i].multiplier;
        }
        Debug.Log("Perfusion pump rate is " + Value + "ml/h");
        return Value;
    }

    public void Right()
    {
        digits[selector].number.Select(false);
        selector = (selector + 1) % digits.Length;
        digits[selector].number.Select(true);
    }

    public void Left()
    {
        digits[selector].number.Select(false);
        selector = (selector > 0) ? selector - 1 : digits.Length - 1;
        digits[selector].number.Select(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
