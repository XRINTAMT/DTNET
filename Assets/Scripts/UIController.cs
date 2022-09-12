﻿using Autohand;
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
    [SerializeField] private Toggle setGuidesStatus;

    public static float dialogueVolume;
    public static float soundVolume;
    public static int language;
    public static int teleportLeftHand;
    public static int moveLeftHand;
    public static int subtitles;
    public static int guides;

    [SerializeField] private AppSettings appSettings;
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
/*        appSettings.UpdateSettings()*/;
    }

    public void SetSoundVolume()
    {
        soundVolume = setSoundVolumeStatus.value;
        //appSettings.UpdateSettings();
    }

    public void SetLanguage(int languageIndex)
    {
        //language = setLanguageStatus.value;
        language = languageIndex;
        //appSettings.UpdateSettings();
        return;
    }

    public void SetTeleportHand(int handIndex) 
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        teleportLeftHand = handIndex;
        appSettings.UpdateSettings();
        return;
    }

    public void SetMoveHand(int handIndex)
    {
        //moveLeftHand = setMovetHandStatus.value;
        teleportLeftHand = handIndex;
        //appSettings.UpdateSettings();
        return;
    }

    public void SetSubtitles()
    {
        if (setSubstitlesStatus.isOn) subtitles=0;
        if (!setSubstitlesStatus.isOn) subtitles = 1;
        //appSettings.UpdateSettings();
    }

    public void SetGuides()
    {
        if (setGuidesStatus.isOn) guides = 0;
        if (!setGuidesStatus.isOn) guides = 1;
        //appSettings.UpdateSettings();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}