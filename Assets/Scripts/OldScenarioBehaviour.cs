using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class OldScenarioBehaviour : MonoBehaviour
{
    [SerializeField] Scenario Scenario;

    void Start()
    {
        Scenario.SetGuidedMode(PlayerPrefs.GetInt("GuidedMode", 1) == 1);
        Scenario.Reconnect();
    }

    public void Activate(bool _active)
    {
        Scenario.Activate(_active);
    }

    public void CalculateScoreManually()
    {
        Scenario.RecieveScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Scenario.Activate(false);
    }
}
