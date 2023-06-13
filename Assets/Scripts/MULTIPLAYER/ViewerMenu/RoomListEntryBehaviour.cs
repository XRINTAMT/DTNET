using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListEntryBehaviour : MonoBehaviour
{
    string roomName;
    [SerializeField] Text RoomNameDisplay;

    void Start()
    {
        
    }

    public void SetUp(string _roomName)
    {
        roomName = _roomName;
        RoomNameDisplay.text = roomName;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
