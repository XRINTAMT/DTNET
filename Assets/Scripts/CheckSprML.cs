using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSprML : MonoBehaviour
{
    [SerializeField] int countML;
    [SerializeField] Text countText;
    [SerializeField] Text textEnd;

    [SerializeField] GameObject endArea;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (int.Parse(textEnd.text)>= countML &&!end)
        {
            textEnd.text = "Congratulations! You have completed the training! Go to the door to finish and return to the main menu!";
            endArea.SetActive(true);
            end = true;
        }
    }
}
