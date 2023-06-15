using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class HygeneStationPhoton : MonoBehaviour
{
    [SerializeField] GameObject waterEffect;
    [SerializeField] GameObject soapEffect;
    HandTriggerAreaEvents handTriggerAreaEvents;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp) 
        {
            AnimationsController animationsController = FindObjectOfType<AnimationsController>();
            handTriggerAreaEvents = GetComponent<HandTriggerAreaEvents>();

            animationsController.waterCondition += WaterCondition;
            handTriggerAreaEvents.HandEnter.AddListener(SoapCondition);
            handTriggerAreaEvents.HandExit.AddListener(SoapConditionStop);
        } 
    }

    void SoapCondition(Hand hand)
    {
        bool condition = true;
        GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.Others, condition);
    }
    void SoapConditionStop(Hand hand)
    {
        bool condition = false;
        GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.Others, condition);
    }

    void WaterCondition(bool condition) 
    {
        GetComponent<PhotonView>().RPC("WaterConditionRPC", RpcTarget.Others, condition);
    }

    [PunRPC]
    void WaterConditionRPC(bool condition) 
    {
        if (PhotonManager._viewerApp)
        {
            if (waterEffect)
                waterEffect.SetActive(condition);
        }
    }

    [PunRPC]
    void SoapConditionRPC(bool condition)
    {
        if (PhotonManager._viewerApp)
        {
            if (soapEffect)
                soapEffect.SetActive(condition);
        }
    }

}
