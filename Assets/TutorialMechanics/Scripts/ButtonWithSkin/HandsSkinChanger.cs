using Autohand;
using Autohand.Demo;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum StartHandsType
{
    Default = 1,
    Custom = 2,
}

public class HandsSkinChanger : MonoBehaviour
{
    [SerializeField] private StartHandsType _startHandsType = StartHandsType.Default;
    [SerializeField] private XRHandPlayerControllerLink _xrHandConrollerLink;
    [ShowIf("_startHandsType", StartHandsType.Custom)]
    [Min(0)]
    [SerializeField] private int _numberStartHands = 0;
    [SerializeField] private HandsPair _defaultHandsPair;
    [SerializeField] private List<HandsPair> _listHandsPairs = new List<HandsPair>();
    private HandsPair _activeHandsPair;

    public UnityEvent<Hand> OnHandSkinChange = new UnityEvent<Hand>();
   
    private void Start()
    {
        Initialize();
    }

    public void SetCustomLeftHand(int index)
    {
        SetCustomOneHand(index, true);
    }

    public void SetCustomRightHand(int index)
    {
        SetCustomOneHand(index, false);
    }

    public void SetCustomOneHand(int index, bool isLeft)
    {
        if (index >= _listHandsPairs.Count)
        {
            return;
        }

        SetSkinHand(isLeft, index);
    }

    public void SetCustomOneHand(int index, Hand hand)
    {
        SetCustomOneHand(index, hand.left);
    }

    public void SetCustomPair(int index)
    {
        SetCustomOneHand(index, false);
        SetCustomOneHand(index, true);

        InvokeOnChangeSkinEventPair();
    }

    public void SetDefaultOneHand(bool isLeft)
    {
        SetSkinHand(isLeft);
    }

    public void SetDefaultOneHand(Hand hand)
    {
        SetDefaultOneHand(hand.left);
    }

    public void SetDefaultPair()
    {
        SetDefaultOneHand(true);
        SetDefaultOneHand(false);
        InvokeOnChangeSkinEventPair();
    }

    private void Initialize()
    {
        _activeHandsPair = new HandsPair();
        switch (_startHandsType)
        {
            case StartHandsType.Custom:
                _activeHandsPair.RightHand = _listHandsPairs[_numberStartHands].RightHand;
                _activeHandsPair.LeftHand = _listHandsPairs[_numberStartHands].LeftHand;
                break;
            case StartHandsType.Default:
            default:
                _activeHandsPair.RightHand = _defaultHandsPair.RightHand;
                _activeHandsPair.LeftHand = _defaultHandsPair.LeftHand;
                break;
        }

        _defaultHandsPair.SetActivePair(false);

        if (_listHandsPairs != null && _listHandsPairs.Count > 0)
        {
            foreach (HandsPair pair in _listHandsPairs)
            {
                pair.SetActivePair(false);
            }
        }

        _activeHandsPair.SetActivePair(true);

        _xrHandConrollerLink.moveController = _activeHandsPair.LeftHand.GetComponent<XRHandControllerLink>();
        _xrHandConrollerLink.player.handLeft = _activeHandsPair.LeftHand.GetComponent<Hand>();

        _xrHandConrollerLink.turnController = _activeHandsPair.RightHand.GetComponent<XRHandControllerLink>();
        _xrHandConrollerLink.player.handRight = _activeHandsPair.RightHand.GetComponent<Hand>();
    }

    private void InvokeOnChangeSkinEventPair()
    {
        InvokeOnChangeSkinEventLeft();
        InvokeOnChangeSkinEventRight();
    }

    private void InvokeOnChangeSkinEventLeft()
    {
        if (_activeHandsPair.LeftHand != null)
        {
            OnHandSkinChange?.Invoke(_activeHandsPair.LeftHand.GetComponent<Hand>());
        }
    }

    private void InvokeOnChangeSkinEventRight()
    {
        OnHandSkinChange?.Invoke(_activeHandsPair.RightHand.GetComponent<Hand>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isLeft"></param>
    /// <param name="index"> -1 для скина по умолчанию</param>
    private void SetSkinHand(bool isLeft, int index = -1)
    {
        HandsPair pair;
        if (index == -1)
        {
            pair = _defaultHandsPair;
        }
        else
        {
            pair = _listHandsPairs[index];
        }

        if (isLeft)
        {
            _activeHandsPair.SetActiveLeftHand(false);
            _activeHandsPair.LeftHand = pair.LeftHand;
            _activeHandsPair.SetActiveLeftHand(true);
            _xrHandConrollerLink.moveController = _activeHandsPair.LeftHand.GetComponent<XRHandControllerLink>();
            _xrHandConrollerLink.player.handLeft = _activeHandsPair.LeftHand.GetComponent<Hand>();
            InvokeOnChangeSkinEventLeft();
        }
        else
        {
            _activeHandsPair.SetActiveRightHand(false);
            _activeHandsPair.RightHand = pair.RightHand;
            _activeHandsPair.SetActiveRightHand(true);
            _xrHandConrollerLink.turnController = _activeHandsPair.RightHand.GetComponent<XRHandControllerLink>();
            _xrHandConrollerLink.player.handRight = _activeHandsPair.RightHand.GetComponent<Hand>();
            InvokeOnChangeSkinEventRight();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_numberStartHands < 0)
        {
            _numberStartHands = 0;
        }

        if (_listHandsPairs.Count == 0)
        {
            _numberStartHands = 0;
        }
        else if (_numberStartHands >= _listHandsPairs.Count)
        {
            _numberStartHands = _listHandsPairs.Count - 1;
        }
    }
#endif
}

[Serializable]
public class HandsPair
{
    public GameObject LeftHand;
    public GameObject RightHand;

    public void SetActivePair(bool status)
    {
        LeftHand.SetActive(status);
        RightHand.SetActive(status);
    }

    public void SetActiveLeftHand(bool status)
    {
        LeftHand.SetActive(status);
    }

    public void SetActiveRightHand(bool status)
    {
        RightHand.SetActive(status);
    }
}
