using Autohand;
using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject playerVR;
    [SerializeField] private GameObject playerVRRightHand;
    [SerializeField] private GameObject playerVRLeftHand;
    [SerializeField] private GameObject playerVRRightHandTeleport;
    [SerializeField] private GameObject playerVRLeftHandTeleport;
    [SerializeField] private GameObject GuideCanvas;

    [SerializeField] private List<AudioSource> audioSourceDialogues;
    [SerializeField] private List<AudioSource> audioSourceSounds;
    [SerializeField] private List<Text> allAppText;
    [SerializeField] private List<Text> textsLanguage1;
    [SerializeField] private List<Text> textsLanguage2;
    [SerializeField] private List<Text> textsLanguage3;
    [SerializeField] private List<Text> textSubtitleDialogues;

    public AppSettings appSettings;

    // Start is called before the first frame update
    void Start()
    {
        appSettings = FindObjectOfType<AppSettings>();
        if (appSettings!=null)
        {
            UpdateState();
        }
        
    }


    public void UpdateState()
    {
        SetVolumeDialogue(appSettings.dialogueVolume);
        SetVolumeSound(appSettings.soundVolume);
        SetLanguage(appSettings.language);
        SetLocomotionType(appSettings.locomotion);
        SetGuide(appSettings.guides);
    }

    public void SetVolumeDialogue(float volumeDialogue)
    {
        if (appSettings != null) for (int i = 0; i < audioSourceDialogues.Count; i++) audioSourceDialogues[i].volume = volumeDialogue;
    }
    public void SetVolumeSound(float volumeSound)
    {
        if (appSettings != null) for (int i = 0; i < audioSourceSounds.Count; i++) audioSourceSounds[i].volume = volumeSound;
    }

    public void SetLanguage(Language language)
    {
        if (appSettings != null && language == 0) for (int i = 0; i < allAppText.Count; i++) allAppText[i] = textsLanguage1[i];

        if (appSettings != null && language == (Language)1) for (int i = 0; i < allAppText.Count; i++) allAppText[i] = textsLanguage2[i];

        if (appSettings != null && language == (Language)2) for (int i = 0; i < allAppText.Count; i++) allAppText[i] = textsLanguage3[i];
    }


    public void SetLocomotionType(Locomotion locoType)
    {
        PlayerPrefs.SetInt("MovementType", (int)locoType);
        Debug.Log("Saved movement type as " + (int)locoType);
        FindObjectOfType<XRMovementControls>().SwitchLocomotion((int)locoType);
    }

    public void SetSubtitles(Subtitles subtitles)
    {
        if (subtitles == 0) for (int i = 0; i < textSubtitleDialogues.Count; i++) textSubtitleDialogues[i].gameObject.SetActive(true);
        if (subtitles == (Subtitles)1) for (int i = 0; i < textSubtitleDialogues.Count; i++) textSubtitleDialogues[i].gameObject.SetActive(false);
    }

    public void SetGuide(Guide guide)
    {
        Debug.Log(guide);
        if (guide == 0) 
        {
            GuideCanvas.SetActive(true);
        }
        if (guide == (Guide)1) 
        {
            GuideCanvas.SetActive(false);
        } 
    }
}
