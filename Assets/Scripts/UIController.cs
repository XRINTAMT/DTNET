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
    [SerializeField] private Toggle setGuidesStatus;
    [SerializeField] private GameObject teleportChosen;
    [SerializeField] private GameObject smoothChosen;
    [SerializeField] private GameObject englishChosen;
    [SerializeField] private GameObject germanChosen;
    [SerializeField] private GameObject lithuanianChosen;

    public static float dialogueVolume;
    public static float soundVolume;
    public static string language;
    public static int teleport;
    public static int subtitles;
    public static int guides;

    void Start()
    {
        LoadSettingsIntoUI();
    }

    public void LoadSettingsIntoUI()
    {
        setDialogueVolumeStatus.value = PlayerPrefs.GetFloat("dialogueVolume", 0.5f);
        setSoundVolumeStatus.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        setSubstitlesStatus.isOn = PlayerPrefs.GetInt("Subtitles", 0) == 0;
        setGuidesStatus.isOn = PlayerPrefs.GetInt("Subtitles", 0) == 0;
        teleport = PlayerPrefs.GetInt("MovementType", 0);
        language = PlayerPrefs.GetString("Language", "English");
        teleportChosen.SetActive(teleport == 0);
        smoothChosen.SetActive(teleport == 1);
        englishChosen.SetActive(language == "English");
        germanChosen.SetActive(language == "German");
        lithuanianChosen.SetActive(language == "Lithuanian");
    }
    public void SetDialogueVolume() 
    {
        dialogueVolume = setDialogueVolumeStatus.value;
        PlayerPrefs.SetFloat("dialogueVolume", dialogueVolume);
    }

    public void SetSoundVolume()
    {
        soundVolume = setSoundVolumeStatus.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }

    public void SetLanguage(string lang)
    {
        language = lang;
        PlayerPrefs.SetString("Language", lang);
    }

    public void SetLocomotionType(int LocomotionID)
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        teleport = LocomotionID;
        teleportChosen.SetActive(teleport == 0);
        smoothChosen.SetActive(teleport == 1);
        PlayerPrefs.SetInt("MovementType", teleport);
        Object.FindObjectOfType<XRMovementControls>().SwitchLocomotion(teleport);
    }

    public void SetSubtitles()
    {

        if (setSubstitlesStatus.isOn) 
            subtitles = 0;
        else
            subtitles = 1;
        PlayerPrefs.SetInt("Subtitles", subtitles);
    }

    public void SetGuides()
    {
        if (setGuidesStatus.isOn)
            guides = 0;
        else
            guides = 1;
        PlayerPrefs.SetInt("GuidedMode", guides);
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
