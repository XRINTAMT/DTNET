using Autohand;
using System.Collections;
using UnityEngine;

public class DistanceLimiterFromCamera : DistanceLimiter
{
    [Min(0.25f)]
    [SerializeField] protected float _distance = 0.5f;
    [SerializeField] protected Camera _mainCamera;
    protected Coroutine _checkDistance_Coroutine;

    public override void GrabItem(Hand hand, Grabbable grabbable)
    {
        base.GrabItem(hand, grabbable);

        if (_checkDistance_Coroutine == null)
        {
            _checkDistance_Coroutine = StartCoroutine(HandsRelease(0.5f));
        }
    }

    protected override bool CheckDistance(float distance)
    {
        float length = (gameObject.transform.position - _mainCamera.transform.position).magnitude;
        return (length <= distance);
    }

    protected override IEnumerator HandsRelease(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!CheckDistance(_distance))
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
            _checkDistance_Coroutine = StartCoroutine(HandsRelease(0.1f));
        }
    }
}
