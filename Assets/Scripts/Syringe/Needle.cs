using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] Syringe Srg;

    private void OnTriggerEnter(Collider other)
    {
        Ampule medicine;
        if (other.TryGetComponent<Ampule>(out medicine))
        {
            Srg.Inserted(medicine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ampule medicine;
        if (other.TryGetComponent<Ampule>(out medicine))
        {
            Srg.Ejected();
        }
    }
}
