using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioSystem;

public class PerfusionPump : MonoBehaviour
{
    [SerializeField] TaskSpecificValues DataInterface;
    void Start()
    {
        
    }

    public void makeInjection()
    {
        Syringe srg = GetComponentInChildren<Syringe>();
        foreach(string med in srg.ingredients.Keys)
        {
            DataInterface.SendDataItem(med, (int)srg.ingredients[med]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
