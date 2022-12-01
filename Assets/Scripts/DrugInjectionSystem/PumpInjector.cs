using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using ScenarioTaskSystem;

[RequireComponent(typeof(Task))]
public class PumpInjector : MonoBehaviour
{
    public Injection RequiredInjection;
    [SerializeField] PlacePoint SyringeHolder;
    [SerializeField] float AimValue;
    [SerializeField] PerfusionPumpSettings SpeedSetup;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Inject()
    {
        float speed = SpeedSetup.GetValue();
        if (SyringeHolder.placedObject != null && (speed > 0))
        {
            Syringe srg;
            if (SyringeHolder.placedObject.TryGetComponent<Syringe>(out srg))
            {
                if (srg.Lable != null && (speed == AimValue))
                {
                    if (srg.Lable == RequiredInjection)
                    {
                        GetComponent<Task>().Complete(1);
                    }
                    else
                    {
                        GetComponent<Task>().Complete(0);
                    }
                }
                else
                {
                    GetComponent<Task>().Complete(0);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
