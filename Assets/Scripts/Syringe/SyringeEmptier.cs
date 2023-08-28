using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

[RequireComponent(typeof(PlacePoint))]
public class SyringeEmptier : MonoBehaviour
{
    PlacePoint syringeHolder;
    public bool Expired;

    void Start()
    {
        syringeHolder = GetComponent<PlacePoint>();
    }

    public void EmptySyringe(float time)
    {
        if(syringeHolder.placedObject != null)
        {
            Syringe srg;
            if (syringeHolder.placedObject.TryGetComponent<Syringe>(out srg))
            {
                srg.Empty(time);
                Expired = srg.GetComponent<Expirable>().Expired;
            }
        }
    }

    void Update()
    {
        
    }
}
