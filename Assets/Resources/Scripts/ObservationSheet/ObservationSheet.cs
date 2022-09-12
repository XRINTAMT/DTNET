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
                return;
            }
        }
        if(TryGetComponent<Task>(out Task a))
            a.Complete();
        BakeAndContinue();
    }

    public void BakeAndContinue(){
        Toggle[] tickboxes = GetComponentsInChildren<Toggle>();
        for(int i = 0; i < tickboxes.Length; i++){
            if(tickboxes[i].isOn){
                tickboxes[i].graphic.transform.SetParent(transform.parent);
            }
        }
        InputField[] fields = GetComponentsInChildren<InputField>();
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].textComponent.transform.SetParent(transform.parent);
        }
        if (NextRow != null)
        {
            NextRow.SetActive(true);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
