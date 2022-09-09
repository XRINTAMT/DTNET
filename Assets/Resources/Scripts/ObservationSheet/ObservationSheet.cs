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

    void Awake()
    {
        values = new string[RightValues.Length];
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
        for(int i = 0; i < RightValues.Length; i++)
        {
            if(values[i] != RightValues[i])
            {
                Debug.Log("Wrong value in your observation sheet! Item " + i + " should be " + RightValues[i] + "but it is currently " + values[i]);
                return;
            }
        }
        if(TryGetComponent<Task>(out _))
            GetComponent<Task>().Complete();
    }

    public void BakeAndContinue(){
        Toggle[] tickboxes = GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tickboxes.Length; i++){
            if(tickboxes[i].isOn){
                //tickboxes[i].Graphic;
            }
        }
    }

    void Update()
    {
        
    }
}
