using Autohand.Demo;
using UnityEngine;
using UnityEngine.Events;

public class CheckButtonPress : MonoBehaviour
{
    [SerializeField] XRHandControllerLink _controllerLink;

    private bool _isGripButtonPressed = false;
    private bool _isTriggerButtonPressed = false;
    private bool _isThumberStickMoved = false;

    public UnityEvent OnGripButtonPressed = new UnityEvent();
    public UnityEvent OnTriggerButtonPressed = new UnityEvent();
    public UnityEvent OnThumberStickMoved = new UnityEvent();

    private void Update()
    {
        if (_controllerLink.ButtonPressed(CommonButton.gripButton))
        {
            if (!_isGripButtonPressed)
            {
                OnGripButtonPressed?.Invoke();
            }
            _isGripButtonPressed = true;
        }
        else
        {
            _isGripButtonPressed = false;
        }

        if (_controllerLink.ButtonPressed(CommonButton.triggerButton))
        {
            if (!_isTriggerButtonPressed)
            {
                OnTriggerButtonPressed?.Invoke();
            }
            _isTriggerButtonPressed = true;
        }
        else
        {
            _isTriggerButtonPressed = false;
        }

        if (_controllerLink.GetAxis2D(Common2DAxis.primaryAxis) != Vector2.zero)
        {
            if (!_isThumberStickMoved)
            {
                OnThumberStickMoved?.Invoke();
            }
            _isThumberStickMoved = true;
        }
        else
        {
            _isThumberStickMoved = false;
        }
    }
}
