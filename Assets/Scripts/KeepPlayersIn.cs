using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Autohand.Demo;

public class KeepPlayersIn : MonoBehaviour
{
    [SerializeField] LastOkLocation lastLocation;

    private void OnTriggerExit(Collider other)
    {
        AutoHandPlayer _ahPlayer;
        if(other.TryGetComponent<AutoHandPlayer>(out _ahPlayer))
        {
            _ahPlayer.GetComponent<Rigidbody>().position = lastLocation.location;
            _ahPlayer.transform.position = lastLocation.location;
        }
        else
        {
            HeadPhysicsFollower _head;
            if (other.TryGetComponent<HeadPhysicsFollower>(out _head))
            {
                Debug.Log("Someone left the trigger area!!!");
                MoveAHPlayer(_head, lastLocation.location);
            }
        }
    }

    void MoveAHPlayer(HeadPhysicsFollower _head, Vector3 moveTo)
    {
        //moveTo = Vector3.zero;
        AutoHandPlayer _ahPlayer = FindObjectOfType<AutoHandPlayer>();
        Vector3 resultingCoords = moveTo - (_head.transform.position - _ahPlayer.transform.position);
        //Debug.Log("Teleporting from " + AhOffset.transform.position + " to " + resultingCoords);
        //AhOffset.transform.position = resultingCoords;
        Debug.Log("Teleporting to " + moveTo);
        _ahPlayer.GetComponent<Rigidbody>().position = moveTo;
        _ahPlayer.transform.position = moveTo;
    }
}

