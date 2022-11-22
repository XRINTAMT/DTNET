using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
