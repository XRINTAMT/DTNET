using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private string roomName;

    public bool joinRoomOnStart;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        if (joinRoomOnStart) OnJoinedRoom();


    }

 
    public override void OnConnectedToMaster()
    {
        Debug.Log("�� ������������ � �������");
        //base.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("�� ����������� �� �������");
        //base.OnDisconnected(cause);
    }


    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected) return;
       
        roomName = "Room" + Random.Range(1, 100);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("������� �������, ��� �������: " + PhotonNetwork.CurrentRoom.Name);
        //base.OnCreatedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� ������� ������� �������: " + PhotonNetwork.CurrentRoom.Name);
        //base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("ScenarioScene");
        //base.OnJoinedRoom();
    }

    public void JoinRoom() 
    {
        PhotonNetwork.JoinRandomRoom();
    }


}
