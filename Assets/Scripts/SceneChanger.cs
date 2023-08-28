using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    SceneLoader sceneLoader;
    public Action restart;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void Fire(int i)
    {
        if (PhotonManager.offlineMode)
        {
            if (sceneLoader != null) sceneLoader.LoadScene();

            if (i == -1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                SceneManager.LoadScene(i);
        }
        if (!PhotonManager.offlineMode)
        {
            if (i == -1)
            {
                PhotonManager.roomName = PhotonNetwork.CurrentRoom.Name;
                PhotonManager.restart = true;
           
            }

            restart?.Invoke();
            //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            //PhotonNetwork.LeaveRoom();

            PhotonNetwork.LeaveRoom();
            if (i != -1)
                PhotonNetwork.Disconnect();

            SceneManager.LoadScene(0);

        }
       

    }
}
