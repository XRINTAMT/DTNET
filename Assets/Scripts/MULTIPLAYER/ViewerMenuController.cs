using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ViewerMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer AppMixer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject eventSystem;
    [SerializeField] public GameObject settingsMenu;
    [SerializeField] Text roomNumber;
    [SerializeField] Button leaveRoom;
    [SerializeField] Button settings;
    [SerializeField] PlayerViewerMovement Movement;
    public Slider setDialogueVolumeStatus;
    public Slider setSoundVolumeStatus;
    public Slider setMusicVolumeStatus;
    UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UIController>();

        uiController.setDialogueVolumeStatus = setDialogueVolumeStatus;
        uiController.setMusicVolumeStatus = setMusicVolumeStatus;
        uiController.setMusicVolumeStatus = setMusicVolumeStatus;

        FindObjectOfType<EventSystem>().gameObject.SetActive(false);
        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        leaveRoom.onClick.AddListener(LeaveRoom);
        eventSystem.SetActive(true);
        Movement = FindObjectOfType<PlayerViewerMovement>();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void SetVolumeDiaologue() 
    {
        uiController.SetDialogueVolume();
    }
    public void SetVolumeMusic()
    {
        uiController.SetMusicVolume();
    }
    public void SetVolumeSounds()
    {
        uiController.SetSoundVolume();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool _pauseActive = pauseMenu.activeSelf || settingsMenu.activeSelf;
            Cursor.lockState = (_pauseActive) ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !_pauseActive;
            if(_pauseActive)
            {
                settingsMenu.SetActive(false);
                pauseMenu.SetActive(false);
                Movement.Active = true;
            }
            else
            {
                pauseMenu.SetActive(true);
                Movement.Active = false;
            }
        }
    }
}
