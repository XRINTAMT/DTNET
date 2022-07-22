using Autohand;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public abstract class AxisLimiter : MonoBehaviour
{
    [SerializeField] protected Rigidbody _body;
    [SerializeField] protected Grabbable _grab;
    protected Vector3 _direction;
    protected List<Hand> _hands;

    private bool _isGrab = false;

    protected virtual void Start()
    {
        _hands = new List<Hand>();
        _body.isKinematic = true;
        _grab.onGrab.AddListener(GrabItem);
        _grab.onRelease.AddListener(ReleaseItem);
    }

    private void OnDestroy()
    {
        _grab.onGrab.RemoveListener(GrabItem);
        _grab.onRelease.RemoveListener(ReleaseItem);
    }

    protected void FixedUpdate()
    {
        if (_isGrab)
        {
            Moving();
        }
    }

    protected abstract void Moving();

    private void GrabItem(Hand hand, Grabbable grabbable)
    {
        _isGrab = true;
        hand.enableMovement = false;
        hand.body.isKinematic = true;
        _hands = _grab.GetHeldBy();
    }

    private void ReleaseItem(Hand hand, Grabbable grabbable)
    {
        hand.enableMovement = true;
        hand.body.isKinematic = false;
        _hands = _grab.GetHeldBy();
        if (_hands.Count == 0)
        {
            _isGrab = false;
        }
    }
}
