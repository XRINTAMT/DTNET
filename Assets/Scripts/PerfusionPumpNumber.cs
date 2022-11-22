using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfusionPumpNumber : MonoBehaviour
{
    [SerializeField] Color BackgroundColor;
    [SerializeField] Color TextColor;
    private Image background;
    private Text number;
    public int Value { get; private set; } = 0;

    void Awake()
    {
        background = GetComponent<Image>();
        number = GetComponentInChildren<Text>();
    }

    public void Up()
    {
        Value = (Value + 1) % 10;
        number.text = Value.ToString();
    }

    public void Down()
    {
        Value = (Value > 0) ? Value - 1 : 9;
        number.text = Value.ToString();
    }

    public void Select(bool selected)
    {
        if(background != null)
            background.enabled = selected;
        number.color = selected ? BackgroundColor : TextColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
