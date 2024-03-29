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
    public static bool exitToMenu;
    public static bool restart;
    public static bool connectToServer;
    public bool latvianVer;
    void Start()
    {
        roomOptions.MaxPlayers = (byte)maxPlayers;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        offlineMode = true;

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

        //if (roomName == null)
        //    PhotonNetwork.CreateRoom(Random.Range(1000, 9999).ToString(), roomOptions);
        //if (roomName != null) 
        //{
        //    PhotonNetwork.CreateRoom(roomName, roomOptions);
        //}

        PhotonNetwork.CreateRoom(Random.Range(1000, 9999).ToString(), roomOptions);
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


    public void ShowLoadingScreen() 
    {
        if (!connectToServer)
        {
            loadingImage.SetActive(true);
        }
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

        connectToServer = true;
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        connectToServer = false;
        Debug.Log("DISCONNECT SERVER");
    }
    public override void OnJoinedLobby()
    {
        if (automaticJoinRoom && !viewerApp)
            CreateRoom();
    }
    public override void OnCreatedRoom()
    {
        offlineMode = false;
        if((latvianVer == false) || (PlayerPrefs.GetString("Role", "Assistant") == "Nurse"))
        {
            SceneManager.LoadScene("ScenarioScene");
        }
        else
        {
            SceneManager.LoadScene("ScenarioSceneDoctorsAssistant");
        }
        Debug.Log("Create room" + roomName);
        roomName = null;
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
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("LeaveRoom");
    }

    private void Update()
    {
        if (restart)
        {
            if (FindObjectOfType<ViewerMenuBehaviour>().Entries.Count>1)
            {
                JoinRoom();
                restart = false;
            }
        }
    }


}
