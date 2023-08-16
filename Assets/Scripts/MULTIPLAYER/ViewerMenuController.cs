using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
        FindObjectOfType<EventSystem>().gameObject.SetActive(false);
        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        leaveRoom.onClick.AddListener(LeaveRoom);
        eventSystem.SetActive(true);
        Movement = FindObjectOfType<PlayerViewerMovement>();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("ViewerMode");
    }

    public void SetVolumeDiaologue() 
    {
        SetDialogueVolume();
    }
    public void SetVolumeMusic()
    {
        SetMusicVolume();
    }
    public void SetVolumeSounds()
    {
        SetSoundVolume();
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


    public void SetDialogueVolume()
    {
        UIController.dialogueVolume = setDialogueVolumeStatus.value;
        PlayerPrefs.SetFloat("dialogueVolume", UIController.dialogueVolume);
        if (UIController.dialogueVolume == 0)
            AppMixer.SetFloat("Dialogues", -80);
        else
        {
            AppMixer.SetFloat("Dialogues", Mathf.Log(UIController.dialogueVolume) * 20);
        }
    }

    public void SetSoundVolume()
    {
        UIController.soundVolume = setSoundVolumeStatus.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("soundVolume", UIController.soundVolume);
        if (UIController.soundVolume == 0)
            AppMixer.SetFloat("Sounds", -80);
        else
        {
            AppMixer.SetFloat("Sounds", Mathf.Log(UIController.soundVolume) * 20);
        }
    }

    public void SetMusicVolume()
    {
        UIController.musicVolume = setMusicVolumeStatus.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("musicVolume", UIController.musicVolume);
        if (UIController.musicVolume == 0)
            AppMixer.SetFloat("Music", -80);
        else
        {
            AppMixer.SetFloat("Music", Mathf.Log(UIController.musicVolume) * 20);
        }
    }


}
