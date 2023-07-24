using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class Pump_ConnectTubing : MonoBehaviour
{
    [SerializeField] private GameObject IVTube;
    [SerializeField] bool firstTime = true;
    [SerializeField] GameObject ExpiredHint;
    public bool Expired = false;

    public void ReplaceTubing(PlacePoint _point, Grabbable _tubing)
    {
        Expired = _tubing.GetComponent<Expirable>().Expired;
        Hand _correctHand = _tubing.GetHeldBy()[0];

        _correctHand.ForceReleaseGrab();
        IVTube.SetActive(true);

        _correctHand.TryGrab(IVTube.GetComponent<Grabbable>());

        Destroy(_tubing.gameObject);
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
