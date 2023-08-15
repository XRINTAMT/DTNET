using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPhoton : MonoBehaviour
{
    SceneChanger sceneChanger;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

        sceneChanger = FindObjectOfType<SceneChanger>();
        if (!PhotonManager._viewerApp)
        {
            sceneChanger.restart+=Restart;
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

    [PunRPC]
    void RestartRPC()
    {
        Debug.Log("RestartRPC");

        if (PhotonManager._viewerApp)
        {
            PhotonManager.roomName = PhotonNetwork.CurrentRoom.Name;
            PhotonManager.restart = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
