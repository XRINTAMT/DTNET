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
    [SerializeField] GameObject loadingImage;
    [SerializeField] UnityEvent OnLeft;
    [SerializeField] int maxPlayers = 3;
    public bool connectedToServerOnStart;
    public bool automaticJoinRoom;
    public bool viewerApp;
    public static bool _viewerApp;
    public static bool offlineMode = true;
    public static string roomName;
    public static bool restart;
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


        if (!_viewerApp&& restart)
            CreateRoom();
        if (_viewerApp && restart)
            JoinRoom();
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
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (roomName=="")
            PhotonNetwork.CreateRoom(Random.Range(1000, 9999).ToString(), roomOptions);
        else
            PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    public void ConnectToRandomRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        offlineMode = false;
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        offlineMode = false;
        PhotonNetwork.JoinRoom(roomName);
    }

    override public void OnLeftRoom()
    {
        OnLeft.Invoke();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECT TO SERVER");
        if (loadingImage)
            loadingImage.SetActive(false);
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
        offlineMode = false;
        SceneManager.LoadScene("ScenarioScene");
        Debug.Log("Create room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join room FALSE");
        CreateRoom();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
            roomInfo.Add(info);
    }

}
