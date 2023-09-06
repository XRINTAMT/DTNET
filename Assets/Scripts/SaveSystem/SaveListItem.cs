using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveListItem : DataSaver
{
    Text textObj;
    string savedText;
    Color savedColor;

    private void Start()
    {
        textObj = GetComponent<Text>();
        if(textObj == null)
        {
            Destroy(this);
        }
    }

    public override void Load()
    {
        if (textObj == null)
        {
            Destroy(this);
            return;
        }
        if (savedText != null && savedColor != null)
        {
            textObj.color = savedColor;
            textObj.text = savedText;
        }
    }

    public override void Save()
    {
        if (textObj == null)
        {
            Destroy(this);
            return;
        }
        savedColor = textObj.color;
        savedText = textObj.text;
    }
}
