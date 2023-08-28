using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using System;

public class Pump_ConnectTubing : MonoBehaviour
{
    public GameObject IVTube;
    [SerializeField] bool firstTime = true;
    [SerializeField] GameObject ExpiredHint;

    public bool Expired = false;
    public Action<GameObject> connectTubing;


    public void ReplaceTubing(PlacePoint _point, Grabbable _tubing)
    {
        Expired = _tubing.GetComponent<Expirable>().Expired;
        //Debug.Log(_tubing.GetHeldBy().Count);
        Hand _correctHand = _tubing.GetHeldBy()[0];

        _correctHand.ForceReleaseGrab();
        IVTube.SetActive(true);

        _correctHand.TryGrab(IVTube.GetComponent<Grabbable>());

        if (!PhotonManager.offlineMode)
        {
            connectTubing?.Invoke(_tubing.gameObject);
        }

        //Destroy(_tubing.gameObject);
        _tubing.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("GuidedMode", 1) == 1)
        {
            ExpiredHint.SetActive(Expired);
        }

    }

    void Start()
    {
        Invoke("DisableInitTubing", 0.1f);
    }

    public void DisableInitTubing()
    {
        if (firstTime)
        {
            IVTube.SetActive(false);
            firstTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
