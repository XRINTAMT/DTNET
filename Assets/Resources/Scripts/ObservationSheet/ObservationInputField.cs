using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class ObservationInputField : MonoBehaviour
{
    InputField Value;
    [SerializeField] int IDtoChange;
    [SerializeField] ObservationSheet ThisRow;

    void Start()
    {
        Value = GetComponent<InputField>();    
    }

    public void OnChange()
    {
        ThisRow.ChangeValue(IDtoChange, Value.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
