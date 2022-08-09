using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectController : MonoBehaviour
{
    [SerializeField] private GameObject inject;
    public void ActivateInject() 
    {
        inject.SetActive(true);
    }
    public void DisactivateInject()
    {
        inject.SetActive(false);
    }
}
