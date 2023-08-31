using System.Collections;
using System.Collections.Generic;
using Autohand;
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
    [field: SerializeField] public float minValue { get; private set; }
    [field: SerializeField] public float maxValue { get; private set; }
    [SerializeField] int NumberOfValues;
    [SerializeField] int ValueID;
    [SerializeField] Pacer Pacer;

    float lastDegrees = 0;
    float lastSnapDegrees = 0;
    float firstDegrees;


    Hand grabHand;
    Grabbable grabbable;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        firstDegrees = transform.localEulerAngles.y;
        if (firstDegrees > 180)
            firstDegrees = 360 - firstDegrees;


        grabbable = GetComponent<Grabbable>();
        grabbable.onGrab.AddListener(OnGrab);
        grabbable.onRelease.AddListener(OnRelease);
    }


    void OnGrab(Hand hand, Grabbable grabbable)
    {
        grabHand = hand;
    }
    void OnRelease(Hand hand, Grabbable grabbable)
    {
        grabHand = null;
    }

    public void PlayHapticVibration(float time)
    {
        if (grabHand)
        {
            grabHand.PlayHapticVibration(time);
        }
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

        PlayHapticVibration(0.1f);

        monitor.ChangeValue(ValueID, val, 1);
        Pacer.OnValueChanged(ValueID, val);
    }
}