using Autohand;
using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Assets.SimpleLocalization;


public class UIController : MonoBehaviour
{
    public Slider setDialogueVolumeStatus;
    public Slider setSoundVolumeStatus;
    public Slider setMusicVolumeStatus;
    [SerializeField] private Slider setWalkingSpeed;
    //[SerializeField] private Dropdown setLanguageStatus;
    //[SerializeField] private Dropdown setTeleportHandStatus;
    //[SerializeField] private Dropdown setMovetHandStatus;
    [SerializeField] private Toggle setSubstitlesStatus;
    [SerializeField] private Toggle setGuidesStatus;
    [SerializeField] private GameObject teleportChosen;
    [SerializeField] private GameObject smoothChosen;
    [SerializeField] private GameObject mixedChosen;
    [SerializeField] private GameObject englishChosen;
    [SerializeField] private GameObject germanChosen;
    [SerializeField] private GameObject lithuanianChosen;
    [SerializeField] private GameObject latvianChosen;
    [SerializeField] private GameObject swedishChosen;
    [SerializeField] private GameObject assistantChosen;
    [SerializeField] private GameObject nurseChosen;
    [SerializeField] private AudioMixer AppMixer;
    [SerializeField] private Toggle SkippingToggle;

    public static float dialogueVolume;
    public static float soundVolume;
    public static float musicVolume;
    public static float walkingSpeed;
    public static string language;
    public static string role;
    public static int teleport;
    public static int subtitles;
    public static int guides;
    public static bool multiplayerMode;
    SceneLoader sceneLoader;

    XRMovementControls xRMovementControls;
    [SerializeField] List<Button> ButtonsChooseRight = new List<Button>();
    [SerializeField] List<Button> ButtonsChooseLeft = new List<Button>();
    private void Awake()
    {
        xRMovementControls = FindObjectOfType<XRMovementControls>();
    }
    void Start()
    {
        LoadSettingsIntoUI();
        sceneLoader = FindObjectOfType<SceneLoader>();

    }

    public void LoadSettingsIntoUI()
    {
        setDialogueVolumeStatus.value = PlayerPrefs.GetFloat("dialogueVolume", 0.5f);
        setSoundVolumeStatus.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        setMusicVolumeStatus.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        setSubstitlesStatus.isOn = PlayerPrefs.GetInt("Subtitles", 0) == 0;
        teleport = PlayerPrefs.GetInt("MovementType", 2);
        language = PlayerPrefs.GetString("Language", "English");
        role = PlayerPrefs.GetString("Role", "Assistant");
        LocalizationManager.Language = language;
        teleportChosen.SetActive(teleport == 0);
        smoothChosen.SetActive(teleport == 1);
        mixedChosen.SetActive(teleport == 2);
        englishChosen.SetActive(language == "English");
        germanChosen.SetActive(language == "German");
        lithuanianChosen.SetActive(language == "Lithuanian");
        latvianChosen.SetActive(language == "Latvian");
        swedishChosen.SetActive(language == "Swedish");
        assistantChosen.SetActive(role == "Assistant");
        nurseChosen.SetActive(role == "Nurse");
        SkippingToggle.isOn = PlayerPrefs.GetInt("AllowSkippingDialogues", 0) == 1;

        setWalkingSpeed.value = PlayerPrefs.GetFloat("walkingSpeed", 1.5f);
        AutoHandPlayer.movementType = (MovementType)PlayerPrefs.GetInt("MovementType", 2);
        AutoHandPlayer.movementHand = (MovementHand)PlayerPrefs.GetInt("HandType", 0);

        xRMovementControls.movementType = AutoHandPlayer.movementType;
        xRMovementControls.handType = AutoHandPlayer.movementHand;

        SetHandType((int)AutoHandPlayer.movementHand);
    }

    public void SetSkippingDialogues()
    {
        PlayerPrefs.SetInt("AllowSkippingDialogues", SkippingToggle.isOn ? 1 : 0);
    }

    public void SetDialogueVolume() 
    {
        dialogueVolume = setDialogueVolumeStatus.value;
        PlayerPrefs.SetFloat("dialogueVolume", dialogueVolume);
        if (dialogueVolume == 0)
            AppMixer.SetFloat("Dialogues", -80);
        else
        {
            AppMixer.SetFloat("Dialogues", Mathf.Log(dialogueVolume) * 20);
        }
    }

    public void SetSoundVolume()
    {
        soundVolume = setSoundVolumeStatus.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        if (soundVolume == 0)
            AppMixer.SetFloat("Sounds", -80);
        else
        {
            AppMixer.SetFloat("Sounds", Mathf.Log(soundVolume) * 20);
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = setMusicVolumeStatus.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        if (musicVolume == 0)
            AppMixer.SetFloat("Music", -80);
        else
        {
            AppMixer.SetFloat("Music", Mathf.Log(musicVolume) * 20);
        }
    }

    public void SetWalkingSpeed()
    {
        walkingSpeed = setWalkingSpeed.value;
        //appSettings.UpdateSettings();
        PlayerPrefs.SetFloat("walkingSpeed", walkingSpeed);
        Object.FindObjectOfType<XRMovementControls>().SetMovementSpeed(walkingSpeed);
    }

    public void SetLanguage(string lang)
    {
        language = lang;
        PlayerPrefs.SetString("Language", lang);
        LocalizationManager.Language = language;
        englishChosen.SetActive(language == "English");
        germanChosen.SetActive(language == "German");
        lithuanianChosen.SetActive(language == "Lithuanian");
        latvianChosen.SetActive(language == "Latvian");
        swedishChosen.SetActive(language == "Swedish");
    }

    public void SetLocomotionType(int LocomotionID)
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        teleport = LocomotionID;
        teleportChosen.SetActive(teleport == 0);
        smoothChosen.SetActive(teleport == 1);
        PlayerPrefs.SetInt("MovementType", teleport);
        Debug.Log("Looking for a thing");
        //Object.FindObjectOfType<XRMovementControls>().SwitchLocomotion(teleport);
        Debug.Log("found one");
        AutoHandPlayer.movementType = (MovementType)LocomotionID;
        SetHandType((int)AutoHandPlayer.movementHand);
      
    }
    public void SetHandType(int hand)
    {
        PlayerPrefs.SetInt("HandType", hand);
        AutoHandPlayer.movementHand = (MovementHand)hand;
        AutoHandPlayer.movementType = (MovementType)teleport;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        { 
            if (AutoHandPlayer.movementHand == MovementHand.Left) //0
            {
                foreach (Transform child in transform.root.GetComponentsInChildren<Transform>(true))
                {
                    if (child.name == "ButtonHandRight")
                    {
                        child.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }
            if (AutoHandPlayer.movementHand == MovementHand.Right) //1
            {
                foreach (Transform child in transform.root.GetComponentsInChildren<Transform>(true))
                {
                    if (child.name == "ButtonHandLeft")
                    {
                        child.GetComponent<Button>().onClick.Invoke();
                    }
                }

            }
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            xRMovementControls.SetHandType(AutoHandPlayer.movementHand, AutoHandPlayer.movementType);
        }

    }

    public void SetGender(int genderID)
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        PlayerPrefs.SetInt("Gender", genderID);
    }

    public void SetRole(string _role)
    {
        //teleportLeftHand = setTeleportHandStatus.value;
        PlayerPrefs.SetString("Role", _role);
    }

    public void SetSubtitles()
    {

        if (setSubstitlesStatus.isOn) 
            subtitles = 0;
        else
            subtitles = 1;
        PlayerPrefs.SetInt("Subtitles", subtitles);
    }

    public void SetGuides(bool guides)
    {
        PlayerPrefs.SetInt("GuidedMode", guides ? 1 : 0);
        
    }

    public void SetMultiplayerMode() 
    {
        multiplayerMode = !multiplayerMode;
    }

    public void LoadScene(string name)
    {
        if (!multiplayerMode)
        {
            if (sceneLoader != null)
            {
                sceneLoader.LoadScene(name);
            }

            if (sceneLoader == null)
                SceneManager.LoadScene(name);
        }
        if (multiplayerMode)
        {
            FindObjectOfType<PhotonManager>().ConnectToServer();
        }

    }

    public void Exit() 
    {
        Application.Quit();
    }
}
