using UnityEngine;

public enum AxisLimited
{
    X = 1,
    Y = 2,
    Z = 3
}

public class CardanShaftLimiter : MonoBehaviour
{
    [Tooltip("Joint вращательной части")]
    [SerializeField] protected ConfigurableJoint _grabJoint;
    [Tooltip("Joint выдвижной части")]
    [SerializeField] protected ConfigurableJoint _extendableJoint;
    [Min(0f)]
    [Tooltip("ќграничение угла поворота поворотной части")]
    [SerializeField] protected float _angleLimit = 0.05f;
    [Min(0.000001f)]
    [Tooltip("ќграничение рассто€ни€ выдвижной части")]
    [SerializeField] protected float _distanceLimit = 0f;
    [Tooltip("¬ыбор оси, которой будет происходить ограничение перемещени€. ѕо остальным ос€м движение будет недоступно")]
    [SerializeField] protected AxisLimited _limitAxis = AxisLimited.X;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _grabJoint.xMotion = ConfigurableJointMotion.Locked;
        _grabJoint.yMotion = ConfigurableJointMotion.Locked;
        _grabJoint.zMotion = ConfigurableJointMotion.Locked;
        _grabJoint.angularXMotion = ConfigurableJointMotion.Limited;
        _grabJoint.angularYMotion = ConfigurableJointMotion.Limited;
        _grabJoint.angularZMotion = ConfigurableJointMotion.Limited;

        SoftJointLimit minlimit = new SoftJointLimit()
        {
            limit = -_angleLimit,
            bounciness = 0,
            contactDistance = 0,
        };
        _grabJoint.lowAngularXLimit = minlimit;

        SoftJointLimit maxlimit = new SoftJointLimit()
        {
            limit = _angleLimit,
            bounciness = 0,
            contactDistance = 0,
        };
        _grabJoint.highAngularXLimit = maxlimit;
        _grabJoint.angularYLimit = maxlimit;
        _grabJoint.angularZLimit = maxlimit;

        _grabJoint.projectionMode = JointProjectionMode.PositionAndRotation;
        _grabJoint.projectionDistance = 0;
        _grabJoint.projectionAngle = 0;

        _extendableJoint.xMotion = ConfigurableJointMotion.Locked;
        _extendableJoint.yMotion = ConfigurableJointMotion.Locked;
        _extendableJoint.zMotion = ConfigurableJointMotion.Locked;
        _extendableJoint.angularXMotion = ConfigurableJointMotion.Locked;
        _extendableJoint.angularYMotion = ConfigurableJointMotion.Locked;
        _extendableJoint.angularZMotion = ConfigurableJointMotion.Locked;

        switch (_limitAxis)
        {
            case AxisLimited.X:
                _extendableJoint.xMotion = ConfigurableJointMotion.Limited;
                break;
            case AxisLimited.Y:
                _extendableJoint.yMotion = ConfigurableJointMotion.Limited;
                break;
            case AxisLimited.Z:
                _extendableJoint.zMotion = ConfigurableJointMotion.Limited;
                break;
        }

        SoftJointLimit linearLimit = new SoftJointLimit
        {
            limit = _distanceLimit,
            bounciness = 0,
            contactDistance = 0
        };
        _extendableJoint.linearLimit = linearLimit;
        _extendableJoint.connectedBody = _grabJoint.GetComponent<Rigidbody>();
        _extendableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
        _extendableJoint.projectionDistance = 0;
        _extendableJoint.projectionAngle = 0;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Initialize();
    }
#endif
}
