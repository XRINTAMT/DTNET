using Autohand;
using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] private Slider setDialogueVolumeStatus;
    [SerializeField] private Slider setSoundVolumeStatus;
    //[SerializeField] private Dropdown setLanguageStatus;
    //[SerializeField] private Dropdown setTeleportHandStatus;
    //[SerializeField] private Dropdown setMovetHandStatus;
    [SerializeField] private Toggle setSubstitlesStatus;

    public static float dialogueVolume;
    public static float soundVolume;
    public static int language;
    public static int teleportLeftHand;
    public static int moveLeftHand;
    public static int subtitles;

    AppSettings appSettings;
    void Start()
    {
        SetDialogueVolume();
        SetSoundVolume();
        SetLanguage(0);
        SetTeleportHand(0);
        SetMoveHand(0);
        SetSubtitles();

        appSettings = FindObjectOfType<AppSettings>();
        appSettings.UpdateSettings();
    }
    public void SetDialogueVolume() 
    {
        dialogueVolume = setDialogueVolumeStatus.value;
    }

    public void SetSoundVolume()
    {
        soundVolume = setSoundVolumeStatus.value;
    }

    public void SetLanguage(int languageIndex)
    {
        //language = setLanguageStatus.value;
        language = languageIndex;
        return;
    }

    public void SetTeleportHand(int handIndex) 
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        teleportLeftHand = handIndex;
        return;
    }

    public void SetMoveHand(int handIndex)
    {
        //moveLeftHand = setMovetHandStatus.value;
        teleportLeftHand = handIndex;
        return;
    }

    public void SetSubtitles()
    {
        if (setSubstitlesStatus.isOn) subtitles=0;
        if (!setSubstitlesStatus.isOn) subtitles = 1;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
