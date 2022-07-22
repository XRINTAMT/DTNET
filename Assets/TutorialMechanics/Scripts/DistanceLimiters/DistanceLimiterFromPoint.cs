using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class DistanceLimiterFromPoint : DistanceLimiter
{
    [Min(0.25f)]
    public float Distance = 0.5f;

    [SerializeField] private DistanceGrabbable _distanceGrab;
    private bool _isInit = false;
    protected Coroutine _checkDistance_Coroutine;

    [SerializeField] protected bool _releaseWhenExitArea = false;
    [SerializeField] protected GameObject _centerPoint;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    private void OnEnable()
    {     
        StartCoroutine(CheckHandNearPoint());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public override void GrabItem(Hand hand, Grabbable grabbable)
    {
        base.GrabItem(hand, grabbable);

        if (_checkDistance_Coroutine == null)
        {
            _checkDistance_Coroutine = StartCoroutine(HandsRelease(0.5f));
        }
    }

    public override void ReleaseItem(Hand hand, Grabbable grabbable)
    {
        base.ReleaseItem(hand, grabbable);
        hand.enableMovement = true;
        hand.body.isKinematic = false;

        if (_handLeft == null && _handRight == null)
        {
            if (_checkDistance_Coroutine != null)
            {
                StopCoroutine(_checkDistance_Coroutine);
                _checkDistance_Coroutine = null;

                _thisGrabbable.body.useGravity = true;
            }
        }
    }

    protected void Initialize()
    {
        if (_distanceGrab != null)
        {
            _distanceGrab.enabled = false;
        }     

        _isInit = true;
        _thisGrabbable.singleHandOnly = true;
    }

    protected override bool CheckDistance(float distance)
    {
        List<Hand> listHand = _thisGrabbable.GetHeldBy();
        float length = (listHand[0].follow.position - _centerPoint.transform.position).magnitude;
        return (length <= distance);
    }

    protected IEnumerator CheckHandNearPoint()
    {
        while (!_isInit)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Collider[] colliders = Physics.OverlapSphere(_centerPoint.transform.position, Distance);

        bool isHandInZone = false;

        foreach (Collider collider in colliders)
        {
            Hand hand = collider.GetComponent<Hand>();
            if (hand != null)
            {
                isHandInZone = true;
                break;
            }
        }
        if (_distanceGrab != null)
        {
            _distanceGrab.enabled = isHandInZone;
        }
        
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckHandNearPoint());
    }

    protected override IEnumerator HandsRelease(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!CheckDistance(Distance))
        {
            if (_releaseWhenExitArea)
            {
                if (_handLeft != null)
                {
                    _handLeft.Release();
                }

                if (_handRight != null)
                {
                    _handRight.Release();
                }

                _checkDistance_Coroutine = null;
            }
            else
            {
                SetObjectPositionInArea();
                yield return new WaitForSeconds(0.05f);
                _checkDistance_Coroutine = StartCoroutine(HandsRelease(0f));
            }
        }
        else
        {
            List<Hand> handGrabList = _thisGrabbable.GetHeldBy();

            foreach (Hand hand in handGrabList)
            {
                if (hand.left)
                {
                    hand.maxFollowDistance = _handLeftMaxFollowDistance;
                }
                else
                {
                    hand.maxFollowDistance = _handRightMaxFollowDistance;
                }
                hand.enableMovement = true;
                hand.body.isKinematic = false;
            }

            yield return new WaitForSeconds(0.1f);
            _checkDistance_Coroutine = StartCoroutine(HandsRelease(0f));
        }
    }

    private void SetObjectPositionInArea()
    {
        List<Hand> handGrabList = _thisGrabbable.GetHeldBy();
        foreach (Hand hand in handGrabList)
        {
            hand.body.isKinematic = true;
            hand.enableMovement = false;
            hand.maxFollowDistance = 1000f;
        }

        _thisGrabbable.body.useGravity = false;
        SetHandsPosition(handGrabList);
    }

    private void SetHandsPosition(List<Hand> handGrabList)
    {
        foreach (Hand hand in handGrabList)
        {
            Transform controllerTransform = hand.follow;
            float lengthPointToController = (controllerTransform.position - _centerPoint.transform.position).magnitude;
            float segment = Distance / lengthPointToController;
            Vector3 pos = Vector3.Lerp(_centerPoint.transform.position, controllerTransform.position, segment);
            hand.SetHandLocation(pos);
        }
    }
}
