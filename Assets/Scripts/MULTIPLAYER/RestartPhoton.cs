using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPhoton : MonoBehaviour
{
    SceneChanger sceneChangerRestart;
    SceneChanger sceneChangerExit;
    public GameObject player;
    bool startScenario;
    int countOfPlayer;

    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

    
        if (!PhotonManager._viewerApp)
        {
            sceneChangerRestart = GameObject.Find("ButtonRestart").GetComponent<SceneChanger>();
            sceneChangerRestart.restart+= Restart;
            sceneChangerExit = GameObject.Find("ButtonMenu").GetComponent<SceneChanger>();
            sceneChangerExit.restart += Restart;
        }
  
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    void Restart() 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("RestartRPC", RpcTarget.All);
        }
    }
    void Exit()
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("ExitRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void RestartRPC()
    {
        Debug.Log("RestartRPC");

        //if (PhotonManager._viewerApp && !PhotonManager.restart)
        //{
        //    PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        //    PhotonNetwork.DestroyAll();
        //    PhotonNetwork.LeaveRoom();
        //    SceneManager.LoadScene("ViewerMode");
        //    PhotonNetwork.Disconnect();

        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //if (PhotonManager._viewerApp && PhotonManager.restart)
        //{
        //    PhotonManager.roomName = PhotonNetwork.CurrentRoom.Name;
        //    PhotonManager.restart = true;
        //    PhotonNetwork.LeaveRoom();
        //    SceneManager.LoadScene("ViewerMode");
        //}
    }

    //[PunRPC]
    //void ExitRPC()
    //{
    //    Debug.Log("ExitRPC");

    //    if (PhotonManager._viewerApp)
    //    {
    //        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
    //        PhotonNetwork.DestroyAll();
    //        PhotonNetwork.LeaveRoom();
    //        SceneManager.LoadScene("ViewerMode");
    //        PhotonNetwork.Disconnect();

    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {

        if (PhotonManager._viewerApp && FindObjectOfType<MultiplayerController>() && !startScenario)
        {
            countOfPlayer = PhotonNetwork.PlayerList.Length;
            startScenario = true;
        }
        if (PhotonManager._viewerApp && PhotonNetwork.PlayerList.Length < countOfPlayer && startScenario)
        {
            PhotonManager.exitToMenu = true;
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.DestroyAll();


            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("ViewerMode");
            PhotonNetwork.Disconnect();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
