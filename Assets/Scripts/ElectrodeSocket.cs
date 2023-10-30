using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class ElectrodeSocket : MonoBehaviour
{
    public int RequiredPadID;
    Electrode ConnectedElectrode;

    private void Awake()
    {
        if (RequiredPadID == -1)
        {
            gameObject.SetActive(PlayerPrefs.GetInt("GuidedMode", 0) == 0);
        }
    }

    public bool IsConnectedCorrectly()
    {
        if(ConnectedElectrode == null)
        {
            if (RequiredPadID == -1)
                return true;
            else
                return false;
        }
        return (ConnectedElectrode.ID == RequiredPadID);
    }

    public void Connect()
    {
        Invoke("ProcessConnection", Time.fixedDeltaTime);
    }

    private void ProcessConnection()
    {
        Electrode _e = GetComponentInChildren<Electrode>();
        if (_e != null)
        {
            ConnectedElectrode = _e;
            _e.TryGetComponent<PlacePoint>(out PlacePoint _pp);
            if (_pp != null)
            {
                _pp.enabled = true;
            }
        }
        else
        {
            Debug.Log("Somehow there's no Electrode here");
        }
    }

    public void Disconnect()
    {
        ConnectedElectrode = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
