using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipsEstimation : MonoBehaviour
{
    public Transform headTransform;
    public Transform hipsOffsetTransform;
    public Vector3 hipsOffset;

    public float rotationThreshold = 30f;
    public float timeThreshold = 0.5f;
    public float rotationSpeed = 10f;

    private float timeSinceRotationThreshold;
    private Quaternion targetHipsRotation;

    void Start()
    {
        targetHipsRotation = hipsOffsetTransform.rotation;
    }

    void Update()
    {
        Vector3 _offset = headTransform.position + hipsOffset;
        hipsOffsetTransform.position = _offset;
        float _yRotationDiff = Mathf.Abs(headTransform.rotation.eulerAngles.y - hipsOffsetTransform.rotation.eulerAngles.y);

        if (_yRotationDiff > rotationThreshold)
        {
            timeSinceRotationThreshold += Time.deltaTime;

            if (timeSinceRotationThreshold >= timeThreshold)
            {
                Quaternion _newRotation = Quaternion.Euler(0f, headTransform.rotation.eulerAngles.y, 0f);
                targetHipsRotation = _newRotation;
                timeSinceRotationThreshold = 0f;
            }
        }
        else
        {
            timeSinceRotationThreshold = 0f;
        }
        hipsOffsetTransform.rotation = Quaternion.Slerp(hipsOffsetTransform.rotation, targetHipsRotation, rotationSpeed * Time.deltaTime);
    }
}