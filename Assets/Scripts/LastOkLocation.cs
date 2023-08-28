using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class LastOkLocation : MonoBehaviour
{
    public Vector3 location { get; private set; }

    private void Awake()
    {
        location = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        AutoHandPlayer _ahPlayer;
        if (other.TryGetComponent<AutoHandPlayer>(out _ahPlayer))
        {
            location = _ahPlayer.transform.position;
        }
    }
}
