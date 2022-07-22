using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionMove
{
    Up,
    Down,
}

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private DirectionMove _direction = DirectionMove.Up;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _distance = 1f;
    private bool _isInit = false;
    private Vector3 _lowPosition;
    private Vector3 _highPosition;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (_isInit)
        {
            Move();
        }
    }

    private void Initialize()
    {
        if (_startPoint == null)
        {
            return;
        }

        switch (_direction)
        {
            case DirectionMove.Up:
                _highPosition = new Vector3(_startPoint.position.x, _startPoint.position.y + _distance, _startPoint.position.z);
                _lowPosition = new Vector3(_startPoint.position.x, _startPoint.position.y, _startPoint.position.z);
                break;
            case DirectionMove.Down:
                _highPosition = new Vector3(_startPoint.position.x, _startPoint.position.y, _startPoint.position.z);
                _lowPosition = new Vector3(_startPoint.position.x, _startPoint.position.y - _distance, _startPoint.position.z);
                break;
        }
        
        _isInit = true;
    }

    private void Move()
    {
        float dist;
        switch (_direction)
        {
            case DirectionMove.Down:
                dist = _lowPosition.y - transform.position.y;
                if (dist >= 0f)
                {
                    _direction = DirectionMove.Up;
                }
                break;
            case DirectionMove.Up:
                dist = _highPosition.y - transform.position.y;
                if (dist <= 0f)
                {
                    _direction = DirectionMove.Down;
                }
                break;
        }

        ChangePosition();
    }

    private void ChangePosition()
    {
        switch (_direction)
        {
            case DirectionMove.Down:
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0f, _distance, 0f), Time.deltaTime / 3f);
                break;
            case DirectionMove.Up:
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0f, _distance, 0f), Time.deltaTime / 3f);
                break;
        }
    }
}
