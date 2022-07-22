using Autohand;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaderPositionControl : OneAxisLimiter
{
    [Min(2)]
    [SerializeField] protected int _countPositions = 2;
    [Min(0)]
    [SerializeField] protected int _numberStartPositions;
    protected List<Vector3> _listPositions;
    protected int _numberCurrentPosition;
    private float halfStep;

    public UnityEvent<int> OnTargetChange = new UnityEvent<int>();

    protected override void Start()
    {
        base.Start();

        _numberCurrentPosition = _numberStartPositions;
        if (_listPositions == null)
        {
            _listPositions = new List<Vector3>();
        }
        else
        {
            _listPositions.Clear();
        }
        Vector3 step = (_upPoint.localPosition - _downPoint.localPosition) / (_countPositions - 1);
        halfStep = Vector3.Magnitude(step / 2f);

        for (int i = 0; i < _countPositions; i++)
        {
            Vector3 position = i == 0 ? _downPoint.localPosition : _listPositions[i - 1] + step;
            _listPositions.Add(position);
        }
        transform.localPosition = _listPositions[_numberCurrentPosition];
        _grab.onRelease.AddListener(ReleaseItem);
    }

    protected override void Moving()
    {
        base.Moving();
        CheckPosition();
    }

    private void CheckPosition()
    {
        Vector3 position = transform.localPosition;
        int nearestPoint = 0;

        for (int i = 0; i < _listPositions.Count; i++)
        {
            float checkDistance = Vector3.Distance(position, _listPositions[i]);
            if (checkDistance < halfStep)
            {
                nearestPoint = i;
            }
        }

        if (_numberCurrentPosition != nearestPoint)
        {
            _numberCurrentPosition = nearestPoint;
            transform.localPosition = _listPositions[_numberCurrentPosition];
            OnTargetChange?.Invoke(_numberCurrentPosition);
        }
    }

    private void ReleaseItem(Hand hand, Grabbable grabbable)
    {
        if (_grab.GetHeldBy().Count == 0)
        {
            CheckPosition();
        }
    }

    public int GetStatus()
    {
        return _numberCurrentPosition;
    }
    public void EnableLever()
    {
        _body.constraints = RigidbodyConstraints.None;
    }
    public void DisableLever()
    {
        _body.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void SetLeverPosition(int status)
    {
        _numberCurrentPosition = status;
        transform.localPosition = _listPositions[_numberCurrentPosition];
    }
}
