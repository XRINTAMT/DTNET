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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Inject()
    {
        if (SyringeHolder.placedObject != null)
        {
            Syringe srg;
            if (SyringeHolder.placedObject.TryGetComponent<Syringe>(out srg))
            {
                if(srg.Lable != null)
                {
                    if(srg.Lable == RequiredInjection)
                    {
                        GetComponent<Task>().Complete();
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
