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

public class ViewerMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer AppMixer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject eventSystem;
    [SerializeField] Text roomNumber;
    [SerializeField] Button leaveRoom;
    [SerializeField] Button settings;
    public Slider setDialogueVolumeStatus;
    public Slider setSoundVolumeStatus;
    public Slider setMusicVolumeStatus;

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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
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
