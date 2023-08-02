using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewerMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject eventSystem;
    [SerializeField] Text roomNumber;
    [SerializeField] Button leaveRoom;
    [SerializeField] Button settings;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EventSystem>().gameObject.SetActive(false);
        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        leaveRoom.onClick.AddListener(LeaveRoom);
        eventSystem.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
