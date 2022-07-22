using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAxesLimitter : AxisLimiter
{
    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _downPoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    protected override void Start()
    {
        base.Start();
        _direction = Vector3.up + Vector3.forward;
    }

    protected override void Moving()
    {
        Vector3 newPos = transform.InverseTransformPoint(_hands[0].follow.position);
        newPos = Vector3.Scale(newPos, _direction) +
            Vector3.Scale(transform.InverseTransformPoint(_downPoint.position), Vector3.one - _direction);

        Vector3 localUpPos = transform.InverseTransformPoint(_upPoint.position);
        Vector3 localDownPos = transform.InverseTransformPoint(_downPoint.position);
        Vector3 localLeftPos = transform.InverseTransformPoint(_leftPoint.position);
        Vector3 localRightPos = transform.InverseTransformPoint(_rightPoint.position);

        if (newPos.y >= localUpPos.y)
        {
            newPos.y = localUpPos.y;
        }

        if (newPos.y <= localDownPos.y)
        {
            newPos.y = localDownPos.y;
        }

        if (newPos.z >= localRightPos.z)
        {
            newPos.z = localRightPos.z;
        }

        if (newPos.z <= localLeftPos.z)
        {
            newPos.z = localLeftPos.z;
        }

        newPos = transform.TransformPoint(newPos);

        transform.position = newPos;
        _body.MovePosition(newPos);
    }   
}
