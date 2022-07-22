using Autohand;
using Autohand.Demo;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DistanceButtonClick : MonoBehaviour
{
    [SerializeField] protected Grabbable _grab;
    [SerializeField] protected ConfigurableJoint _confJoint;
    [SerializeField] protected Rigidbody _rigid;
    [SerializeField] protected Transform _centerPoint;
    [SerializeField] protected HandsPair _avatarPair;
    [Min(0.01f)]
    [SerializeField] protected float _grabDistance = 0.5f;
    [SerializeField] protected bool _isPressed = false;
    
    private Vector3 _pressPosition;
    private Vector3 _unpressPosition;
    protected Hand _enteredHand;
    protected bool _isContollerButtonClick = false;

    public UnityEvent OnPressed = new UnityEvent();
    public UnityEvent OnUnpressed = new UnityEvent();

    protected void Start()
    {
        if (_avatarPair != null)
        {
            _avatarPair.SetActivePair(false);
        }
        
        Vector3 limitAxes = new Vector3(_confJoint.xMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            _confJoint.yMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            _confJoint.zMotion == ConfigurableJointMotion.Locked ? 0 : 1);
        _pressPosition = limitAxes * -_confJoint.linearLimit.limit;
        _unpressPosition = limitAxes * _confJoint.linearLimit.limit;

        if (_isPressed)
        {
            Press();
        }
        else
        {
            Unpress();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(FindHand());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        CheckDistanceHand();
    }

    private void Update()
    {
        CheckButtonPressed();
    }

    private void Press()
    {
        _isPressed = true;

        transform.localPosition = _pressPosition;

        OnPressed?.Invoke();
    }

    private void Unpress()
    {
        _isPressed = false;

        transform.localPosition = _unpressPosition;

        OnUnpressed?.Invoke();
    }

    private void CheckDistanceHand()
    {
        if (_enteredHand != null)
        {
            float currentDistance = (_enteredHand.follow.position - _centerPoint.position).magnitude;

            if (currentDistance > _grabDistance)
            {
                if (_avatarPair != null)
                {
                    if (_enteredHand.left)
                    {
                        _avatarPair.SetActiveLeftHand(false);
                    }
                    else
                    {
                        _avatarPair.SetActiveRightHand(false);
                    }
                }
                _enteredHand.gameObject.SetActive(true);
                _enteredHand = null;
            }
        }
    }

    private void CheckButtonPressed()
    {
        if (_enteredHand != null)
        {
            XRHandControllerLink controllerLink = _enteredHand.GetComponent<XRHandControllerLink>();

            if (controllerLink.ButtonPressed(CommonButton.primaryButton))
            {
                if (_isContollerButtonClick)
                {
                    return;
                }

                if (_isPressed)
                {
                    Unpress();
                }
                else
                {
                    Press();
                }
                _isContollerButtonClick = true;
            }
            else
            {
                _isContollerButtonClick = false;
            }
        }
    }

    private IEnumerator FindHand()
    {
        yield return new WaitForSeconds(0.1f);
        if (_enteredHand == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_centerPoint.position, _grabDistance);

            foreach (Collider collider in colliders)
            {
                Hand hand = collider.GetComponent<Hand>();

                if (hand != null)
                {
                    float currentDistance = (hand.follow.position - _centerPoint.position).magnitude;
                    if (currentDistance < _grabDistance)
                    {
                        _enteredHand = hand;
                        hand.gameObject.SetActive(false);
                        if (_avatarPair != null)
                        {
                            if (_enteredHand.left)
                            {
                                _avatarPair.SetActiveLeftHand(true);
                            }
                            else
                            {
                                _avatarPair.SetActiveRightHand(true);
                            }
                        }
                        break;
                    }
                }
            }
        }
        StartCoroutine(FindHand());
    }

    public int GetStatus()
    {
        return _isPressed ? 1 : 0;
    }

    public void SetStatus(int status)
    {
        if (status == 0)
        {
            _isPressed = false;
            transform.localPosition = _unpressPosition;
        }
        else
        {
            _isPressed = true;
            transform.localPosition = _pressPosition;
        }
    }
}
