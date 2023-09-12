using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListEntryBehaviour : MonoBehaviour
{
    [HideInInspector]
    public string roomName;
    [SerializeField] Text RoomNameDisplay;

    void Start()
    {
        //if (PhotonManager.roomName != "" && PhotonManager.exitToMenu)
        //{
        //    Debug.Log(PhotonNetwork.CountOfPlayers);
        //    Destroy(gameObject);
        //    PhotonManager.roomName = "";
        //    PhotonManager.exitToMenu = false;
        //}

    }

    public void SetUp(string _roomName)
    {
        roomName = _roomName;
        RoomNameDisplay.text = roomName;
    }

    public void JoinRoom()
    {
        PhotonManager.offlineMode = false;
        PhotonNetwork.JoinRoom(roomName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
