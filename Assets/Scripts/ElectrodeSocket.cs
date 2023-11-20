using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class ElectrodeSocket : MonoBehaviour
{
    //[SerializeField] int RequiredPadID;
    [field: SerializeField] public int x { get; private set; }
    [field: SerializeField] public int y { get; private set; }
    public Electrode ConnectedElectrode { get; private set; }

    private void Awake()
    {
        if (!((x == 0 || x == 2) && (y == 0 || y == 3)))
        {
            gameObject.SetActive(PlayerPrefs.GetInt("GuidedMode", 0) == 0);
        }
    }

    /*
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
    */

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
        ConnectedElectrode.Disconnect();
        ConnectedElectrode = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
