using UnityEngine;

public class OneAxisLimiter : AxisLimiter
{
    [SerializeField] protected Transform _upPoint;
    [SerializeField] protected Transform _downPoint;

    protected override void Start()
    {
        base.Start();
        _direction = Vector3.up;
    }

    protected override void Moving()
    {
        Vector3 newPos = transform.InverseTransformPoint(_hands[0].follow.position);
        newPos = Vector3.Scale(newPos, _direction) + 
            Vector3.Scale(transform.InverseTransformPoint(_downPoint.position), Vector3.one - _direction);

        Vector3 localUpPos = transform.InverseTransformPoint(_upPoint.position);
        Vector3 localDownPos = transform.InverseTransformPoint(_downPoint.position);

        if (newPos.y >= localUpPos.y)
        {
            newPos.y = localUpPos.y;
        }

        if (newPos.y <= localDownPos.y)
        {
            newPos.y = localDownPos.y;
        }

        newPos = transform.TransformPoint(newPos);
        
        transform.position = newPos;
        _body.MovePosition(newPos);
    }
}
