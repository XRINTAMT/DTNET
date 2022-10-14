using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

[RequireComponent(typeof(PlacePoint))]
public class SyringeEmptier : MonoBehaviour
{
    PlacePoint syringeHolder;

    void Start()
    {
        syringeHolder = GetComponent<PlacePoint>();
    }

    public void EmptySyringe()
    {
        if(syringeHolder.placedObject != null)
        {
            Syringe srg;
            if (syringeHolder.placedObject.TryGetComponent<Syringe>(out srg))
            {
                srg.Empty();
            }
        }
    }

    void Update()
    {
        
    }
}
