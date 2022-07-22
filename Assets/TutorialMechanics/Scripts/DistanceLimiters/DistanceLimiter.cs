using Autohand;
using UnityEngine;
using System.Collections;

public abstract class DistanceLimiter : MonoBehaviour
{
    [SerializeField] protected Grabbable _thisGrabbable;
    protected Hand _handLeft;
    protected Hand _handRight;
    protected float _handLeftMaxFollowDistance;
    protected float _handRightMaxFollowDistance;

    protected virtual void Start()
    {
        _thisGrabbable.onGrab.AddListener(GrabItem);
        _thisGrabbable.onRelease.AddListener(ReleaseItem);
    }

    private void OnDestroy()
    {
        _thisGrabbable.onGrab.RemoveListener(GrabItem);
        _thisGrabbable.onRelease.RemoveListener(ReleaseItem);
    }

    public virtual void GrabItem(Hand hand, Grabbable grabbable)
    {
        if (hand.left)
        {
            _handLeft = hand;
            _handLeftMaxFollowDistance = hand.maxFollowDistance;
        }
        else
        {
            _handRight = hand;
            _handRightMaxFollowDistance = hand.maxFollowDistance;
        }
    }

    public virtual void ReleaseItem(Hand hand, Grabbable grabbable)
    {
        if (hand.left)
        { 
            hand.maxFollowDistance = _handLeftMaxFollowDistance;
            _handLeft = null; 
        }
        else
        {
            hand.maxFollowDistance = _handRightMaxFollowDistance;
            _handRight = null;
        }
    }

    protected abstract bool CheckDistance(float distance);

    protected abstract IEnumerator HandsRelease(float delay);
}