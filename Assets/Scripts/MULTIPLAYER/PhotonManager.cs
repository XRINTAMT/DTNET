using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    RoomOptions roomOptions = new RoomOptions();
    public List<RoomInfo> roomInfo = new List<RoomInfo>();

    [SerializeField] UnityEvent OnLeft;
    [SerializeField] int maxPlayers = 3;
    public bool connectedToServerOnStart;
    public bool automaticJoinRoom;
    public bool viewerApp;
    public static bool _viewerApp;
    public static bool offlineMode = true;
    void Start()
    {
        roomOptions.MaxPlayers = (byte)maxPlayers;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        if (connectedToServerOnStart)
            ConnectToServer();

        if (viewerApp) 
        {
            PhotonNetwork.NickName = "viewer";
            _viewerApp = true;
        }
    
        else
            PhotonNetwork.NickName = "player";
    }

    public void ConnectServerAndCreateRoom(int scene) 
    {
        ConnectToServer();

    }
    public void ConnectServerAndCreateOfflineRoom(int scene)
    {
        PhotonNetwork.OfflineMode = true;
        CreateRoom(null);
        ConnectToServer();

    }
    public void Leave()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRoom(string nameRoom)
    {
        if (!PhotonNetwork.IsConnected)
            return;
        PhotonNetwork.CreateRoom(nameRoom, roomOptions);
    }
    public void ConnectToRandomRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        PhotonNetwork.JoinRandomRoom();
    }

    override public void OnLeftRoom()
    {
        OnLeft.Invoke();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECT TO SERVER");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("DISCONNECT SERVER");
    }
    public override void OnJoinedLobby()
    {
        if (automaticJoinRoom)
            ConnectToRandomRoom();
    }
    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("MultiplayerScene");
        Debug.Log("Create room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join room FALSE");
        CreateRoom(null);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
            roomInfo.Add(info);
    }

}
