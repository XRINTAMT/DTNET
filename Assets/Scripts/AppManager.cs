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

    [SerializeField] private List<AudioSource> audioSourceDialogues;
    [SerializeField] private List<AudioSource> audioSourceSounds;
    [SerializeField] private List<Text> allAppText;
    [SerializeField] private List<Text> textsLanguage1;
    [SerializeField] private List<Text> textsLanguage2;
    [SerializeField] private List<Text> textsLanguage3;
    [SerializeField] private List<Text> textSubtitleDialogues;

    AppSettings appSettings;
    // Start is called before the first frame update
    void Start()
    {
        appSettings = FindObjectOfType<AppSettings>();
    }


    public void UpdateState()
    {
        SetVolumeDialogue(appSettings.dialogueVolume);
        SetVolumeSound(appSettings.soundVolume);
        SetLanguage(appSettings.language);
        SetTeleportHand(appSettings.teleportHand);
        SetMoveHand(appSettings.moveHand);

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


    public void SetTeleportHand(TeleportHand teleportHand)
    {
        if (appSettings != null && teleportHand == 0)
        {
            playerVRRightHand.transform.Find("TeleporterPointer").gameObject.SetActive(true);
            playerVRLeftHand.transform.Find("TeleporterPointer").gameObject.SetActive(false);
            return;
        }
        if (appSettings != null && teleportHand == (TeleportHand)1)
        {
            playerVRRightHand.transform.Find("TeleporterPointer").gameObject.SetActive(false);
            playerVRLeftHand.transform.Find("TeleporterPointer").gameObject.SetActive(true);
            return;
        }
    }

    public void SetMoveHand(MoveHand moveHand)
    {
        if (appSettings != null && moveHand == 0)
        {
            FindObjectOfType<XRHandPlayerControllerLink>().moveController = playerVRRightHand.GetComponent<XRHandControllerLink>();
            FindObjectOfType<XRHandPlayerControllerLink>().turnController = playerVRLeftHand.GetComponent<XRHandControllerLink>();
            //playerVR.GetComponent<XRHandPlayerControllerLink>().moveController = playerVRRightHand.GetComponent<XRHandControllerLink>();
            //playerVR.GetComponent<XRHandPlayerControllerLink>().turnController = playerVRLeftHand.GetComponent<XRHandControllerLink>();
            return;
        }
        if (appSettings != null && moveHand == (MoveHand)1)
        {
            FindObjectOfType<XRHandPlayerControllerLink>().moveController = playerVRLeftHand.GetComponent<XRHandControllerLink>();
            FindObjectOfType<XRHandPlayerControllerLink>().turnController = playerVRRightHand.GetComponent<XRHandControllerLink>();
            //playerVR.GetComponent<XRHandPlayerControllerLink>().moveController = playerVRLeftHand.GetComponent<XRHandControllerLink>();
            //playerVR.GetComponent<XRHandPlayerControllerLink>().turnController = playerVRRightHand.GetComponent<XRHandControllerLink>();
            return;
        }
    }

    public void SetSubtitles(Subtitles subtitles)
    {
        if (subtitles == 0) for (int i = 0; i < textSubtitleDialogues.Count; i++) textSubtitleDialogues[i].gameObject.SetActive(true);
        if (subtitles == (Subtitles)1) for (int i = 0; i < textSubtitleDialogues.Count; i++) textSubtitleDialogues[i].gameObject.SetActive(false);
    }
}
