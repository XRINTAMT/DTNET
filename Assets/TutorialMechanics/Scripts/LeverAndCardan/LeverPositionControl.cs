using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ConfigurableJoint))]
public class LeverPositionControl : MonoBehaviour
{
    [Range(-177, 172)]
    [SerializeField] protected float _minAngle;
    [Range(-172, 177)]
    [SerializeField] protected float _maxAngle;
    [Min(2)]
    [SerializeField] protected int _countPositions = 2;
    [Min(0)]
    [SerializeField] protected int _numberStartPositions;
    [SerializeField] protected Rigidbody _rigid;
    [SerializeField] protected Grabbable _grab;
    [SerializeField] protected ConfigurableJoint _joint;
    protected List<Vector3> _listAnglePositions;
    protected bool _isInit = false;
    protected int _numberCurrentPosition;
    private bool _isHasReceiverPosition = false;
    private int _receivedPosition = 0;

    public UnityEvent<int> OnTargetChange = new UnityEvent<int>();

    void Start()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        if (_isInit)
        {
            CheckPosition();
        }
    }

    private IEnumerator DelayInit()
    {
        yield return new WaitForSeconds(0.5f);
        _rigid.isKinematic = true;
        if (_isHasReceiverPosition)
        {
            _isHasReceiverPosition = false;
            _numberCurrentPosition = _receivedPosition;
            transform.rotation = Quaternion.Euler(_listAnglePositions[_numberCurrentPosition]);
        }
        _isInit = true;
    }

    private void CheckPosition()
    {
        Quaternion rot = transform.localRotation;
        int nearestPoint = 0;
        float angle = 0f;

        for (int i = 0; i < _listAnglePositions.Count; i++)
        {
            float checkAngle = Quaternion.Angle(rot, Quaternion.Euler(_listAnglePositions[i]));
            if (i == 0)
            {
                nearestPoint = i;
                angle = checkAngle;
            }
            else
            {
                if (checkAngle < angle)
                {
                    angle = checkAngle;
                    nearestPoint = i;
                }
            }
        }

        if (_numberCurrentPosition != nearestPoint)
        {
            _numberCurrentPosition = nearestPoint;

            SetTargetToPoint(_numberCurrentPosition);
            OnTargetChange?.Invoke(_numberCurrentPosition);
        }
    }

    private void SetParams()
    {
        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Locked;
        _joint.zMotion = ConfigurableJointMotion.Locked;
        _joint.configuredInWorldSpace = false;

        Vector3 axis = Vector3.zero;

        if (_joint.angularXMotion == ConfigurableJointMotion.Limited)
        {
            axis = Vector3.right;
        }
        else if (_joint.angularYMotion == ConfigurableJointMotion.Limited)
        {
            axis = Vector3.up;
        }
        else if (_joint.angularZMotion == ConfigurableJointMotion.Limited)
        {
            axis = Vector3.forward;
        }

        _joint.lowAngularXLimit = new SoftJointLimit() { limit = _minAngle };
        _joint.highAngularXLimit = new SoftJointLimit() { limit = _maxAngle };
        _joint.angularYLimit = new SoftJointLimit() { limit = _maxAngle };
        _joint.angularZLimit = new SoftJointLimit() { limit = _maxAngle };

        _numberCurrentPosition = _numberStartPositions;
        if (_listAnglePositions == null)
        {
            _listAnglePositions = new List<Vector3>();
        }
        else
        {
            _listAnglePositions.Clear();
        }

        float step = (_maxAngle - _minAngle) / (_countPositions - 1);

        for (int i = 0; i < _countPositions; i++)
        {
            Vector3 rot;
            if (i == 0)
            {
                rot = transform.localEulerAngles + axis * _minAngle;
                _listAnglePositions.Add(rot);
            }
            else
            {
                rot = _listAnglePositions[i - 1] + axis * step;
                _listAnglePositions.Add(rot);
            }
        }

        Quaternion rotate = Quaternion.Euler(_listAnglePositions[_numberCurrentPosition]);

        transform.localRotation = rotate;
        _rigid.rotation = transform.rotation;

        SetTargetToPoint(_numberCurrentPosition);
    }

    private void SetTargetToPoint(int numbPoint)
    {
        int nPoint = numbPoint;
        if (nPoint >= _listAnglePositions.Count)
        {
            nPoint = _listAnglePositions.Count - 1;
        }

        if (nPoint < 0)
        {
            nPoint = 0;
        }

        Quaternion rot = Quaternion.Euler(_listAnglePositions[nPoint]);
        _joint.targetRotation = rot;
    }

    protected void Initialize()
    {
        SetParams();
        _grab.onGrab.AddListener(GrabItem);
        _grab.onRelease.AddListener(ReleaseItem);

        StartCoroutine(DelayInit());
    }

    public void GrabItem(Hand hand, Grabbable grabbable)
    {
        _rigid.isKinematic = false;
    }

    public void ReleaseItem(Hand hand, Grabbable grabbable)
    {
        if (_grab.GetHeldBy().Count == 0)
        {
            _rigid.isKinematic = true;

            Quaternion rotate = Quaternion.Euler(_listAnglePositions[_numberCurrentPosition]);

            transform.localRotation = rotate;
            _rigid.rotation = transform.rotation;
        }
    }

    public int GetStatus()
    {
        return _numberCurrentPosition;
    }
    public void EnableLever()
    {
        _rigid.constraints = RigidbodyConstraints.None;
    }
    public void DisableLever()
    {
        _rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void SetLeverPosition(int status)
    {
        if (_isInit)
        {
            _numberCurrentPosition = status;
            transform.rotation = Quaternion.Euler(_listAnglePositions[_numberCurrentPosition]);
        }
        else
        {
            _isHasReceiverPosition = true;
            _receivedPosition = status;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_numberStartPositions >= _countPositions)
        {
            _numberStartPositions = _countPositions - 1;
        }

        if (_minAngle > _maxAngle - 5)
        {
            _minAngle = _maxAngle - 5;
        }

        if (_minAngle < -177)
        {
            _minAngle = -177;
        }

        if (_maxAngle < _minAngle + 5)
        {
            _maxAngle = _minAngle + 5;
        }

        if (_maxAngle > 177)
        {
            _maxAngle = 177;
        }

        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Locked;
        _joint.zMotion = ConfigurableJointMotion.Locked;

        _joint.lowAngularXLimit = new SoftJointLimit() { limit = _minAngle };
        _joint.highAngularXLimit = new SoftJointLimit() { limit = _maxAngle };
        _joint.angularYLimit = new SoftJointLimit() { limit = _maxAngle };
        _joint.angularZLimit = new SoftJointLimit() { limit = _maxAngle };
    }
#endif
}
