using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class HygeneStationPhoton : MonoBehaviour
{
    [SerializeField] GameObject waterEffect;
    [SerializeField] GameObject soapEffect;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimationsController animationsController = FindObjectOfType<AnimationsController>();
        animationsController.waterCondition += WaterCondition;
        HandTriggerAreaEvents handTriggerAreaEvents=GetComponent<HandTriggerAreaEvents>();
        handTriggerAreaEvents.HandEnter.AddListener(SoapCondition);
    }

    void SoapCondition(Hand hand)
    {
        bool condition = true;
        GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.Others, condition);
    }
    
    void WaterCondition(bool condition) 
    {
        GetComponent<PhotonView>().RPC("WaterConditionRPC", RpcTarget.Others, condition);
    }

    [PunRPC]
    void WaterConditionRPC(bool condition) 
    {
        if (waterEffect)
            waterEffect.SetActive(condition);
    }

    [PunRPC]
    void SoapConditionRPC(bool condition)
    {
        if (soapEffect)
            soapEffect.SetActive(condition);
    }

}
