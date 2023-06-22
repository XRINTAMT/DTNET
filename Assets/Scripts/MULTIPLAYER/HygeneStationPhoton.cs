using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class HygeneStationPhoton : MonoBehaviour
{
    [SerializeField] GameObject waterEffect;
    [SerializeField] GameObject soapEffect;
    [SerializeField] HandTriggerAreaEvents handTriggerAreaEvents;
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


        ParticleSystem[] particleSystem = FindObjectsOfType<ParticleSystem>(true);

 
        soapEffect = particleSystem[0].gameObject;
        waterEffect = particleSystem[1].gameObject;
        handTriggerAreaEvents = GameObject.Find("SoapCollider").GetComponent<HandTriggerAreaEvents>();

        if (!PhotonManager._viewerApp) 
        {
            AnimationsController animationsController = FindObjectOfType<AnimationsController>();
            //handTriggerAreaEvents = GetComponent<HandTriggerAreaEvents>();

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
            GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.All, condition);
        }
    }
    void SoapAreaExit(Hand hand)
    {
        if (!PhotonManager._viewerApp)
        {
            bool condition = false;
            GetComponent<PhotonView>().RPC("SoapConditionRPC", RpcTarget.All, condition);
        }
    }

    void WaterCondition(bool condition) 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("WaterConditionRPC", RpcTarget.All, condition);
        }
    }

    [PunRPC]
    void WaterConditionRPC(bool condition) 
    {
        Debug.Log("WaterEvent_RPC");
        if (PhotonManager._viewerApp)
        {
            if (waterEffect)
                waterEffect.SetActive(condition);
        }
    }

    [PunRPC]
    void SoapConditionRPC(bool condition)
    {
        Debug.Log("SoapEvent_RPC");
        if (PhotonManager._viewerApp)
        {
            if (soapEffect)
                soapEffect.SetActive(condition);
        }
    }

}
