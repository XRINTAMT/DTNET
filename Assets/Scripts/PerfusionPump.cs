using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioSystem;

public class PerfusionPump : MonoBehaviour
{
    [SerializeField] TaskSpecificValues DataInterface;
    [SerializeField] Pump_ConnectTubing TubingContainer;
    [SerializeField] SyringeEmptier SyringeContainer;

    void Start()
    {
        
    }

    public void makeInjection()
    {
        if((SyringeContainer.Expired || TubingContainer.Expired) && (PlayerPrefs.GetInt("GuidedMode") == 1))
        {
            FindObjectOfType<RestartSystem>().Load();
            return;
        }
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
