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
        waterEffect = GameObject.Find("WaterAnimation");
        soapEffect = GameObject.Find("SoapAnimation");

        if (!PhotonManager._viewerApp) 
        {
            AnimationsController animationsController = FindObjectOfType<AnimationsController>();
            //handTriggerAreaEvents = GetComponent<HandTriggerAreaEvents>();
            handTriggerAreaEvents = GameObject.Find("SoapCollider").GetComponent<HandTriggerAreaEvents>();

            animationsController.waterCondition += WaterCondition;
            handTriggerAreaEvents.HandEnter.AddListener(SoapAreaEnter);
            handTriggerAreaEvents.HandExit.AddListener(SoapAreaExit);
        } 
    }

    void SoapAreaEnter(Hand hand)
    {
        if (!PhotonManager._viewerApp)
        {
            bool condition = true;
            GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.Others, condition);
        }
    }
    void SoapAreaExit(Hand hand)
    {
        if (!PhotonManager._viewerApp)
        {
            bool condition = false;
            GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.Others, condition);
        }
    }

    void WaterCondition(bool condition) 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("WaterConditionRPC", RpcTarget.Others, condition);
        }
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
