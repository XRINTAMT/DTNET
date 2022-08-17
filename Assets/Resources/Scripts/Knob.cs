using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knob: MonoBehaviour
{
    [Tooltip("Snap the Y rotation to the nearest")]
    [SerializeField] public float SnapDegrees;


    [Tooltip("Randomize pitch of SnapSound by this amount")]
    public float RandomizePitch = 0.001f;

    Rigidbody rigid;

    [SerializeField] VitalsMonitor monitor;
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] int NumberOfValues;
    [SerializeField] int ValueID;

    float lastDegrees = 0;
    float lastSnapDegrees = 0;
    float firstDegrees;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        firstDegrees = transform.localEulerAngles.y;
        if (firstDegrees > 180)
            firstDegrees = 360 - firstDegrees;
    }

    void Update()
    {
        float degrees = transform.localEulerAngles.y;
        if (degrees > 180)
            degrees = 360 - degrees;
        degrees = Mathf.Abs(degrees - firstDegrees);
        lastDegrees = degrees;


        float nearestSnap = Mathf.Round(degrees / SnapDegrees) * SnapDegrees;
        if (nearestSnap != lastSnapDegrees)
        {
            OnSnapChange(Mathf.Lerp(minValue,maxValue,nearestSnap/NumberOfValues/SnapDegrees));
        }
        lastSnapDegrees = nearestSnap;
    }

    public void OnSnapChange(float val)
    {
        GetComponent<AudioSource>().Play();

        monitor.ChangeValue(ValueID, val, 1);
    }
}