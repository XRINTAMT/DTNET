using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientData : MonoBehaviour
{
    public float weight { private set; get; }
    [SerializeField] float WeightMin, WeightMax;

    void Awake()
    {
        weight = Random.Range(WeightMin, WeightMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
