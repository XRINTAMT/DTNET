using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject MenuOffset;
    [SerializeField] GameObject Congrats;
    [SerializeField] GameObject CongratsExam;
    [SerializeField] GameObject Main;
    [SerializeField] Camera UICamera;
    [SerializeField] Camera MainCamera;
    [SerializeField] GameObject[] TurnOffOnPause;
    List<AudioSource> AudiosWerePlaying;
    List<Animation> AnimationsWerePlaying;
    XRMovementControls Controls;
    bool ScenarioCompleted = false;
    void Awake()
    {
        AudiosWerePlaying = new List<AudioSource>();
        AnimationsWerePlaying = new List<Animation>();
        Controls = FindObjectOfType<XRMovementControls>();
    }

    private void Start()
    {
        PauseMenu.SetActive(false);
    }
    public void ShowOutroMessage()
    {
        AppPause();
        ScenarioCompleted = true;
        if(PlayerPrefs.GetInt("GuidedMode", 1) == 1)
        {
            Congrats.SetActive(true);
        }
        else
        {
            CongratsExam.SetActive(true);
        }
        Main.SetActive(false);
    }

    public void AppPause()
    {
        AudioSource[] allsources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource Audio in allsources)
        {
            if (Audio.isPlaying)
            {
                AudiosWerePlaying.Add(Audio);
                Audio.Pause();
            }
        }
        foreach(GameObject _obj in TurnOffOnPause)
        {
            _obj.SetActive(false);
        }
        /*
        Animation[] allanimations = FindObjectsOfType<Animation>();
        foreach (Animation Animation in allanimations)
        {
            if (Animation.isPlaying)
            {
                AnimationsWerePlaying.Add(Animation);
                Animation.Stop();
            }
        }
        */
        UICamera.gameObject.GetComponent<Camera>().enabled = true;
        UICamera.cullingMask = (1 << LayerMask.NameToLayer("Hand")) | (1 << LayerMask.NameToLayer("MenusUI")) | (1 << LayerMask.NameToLayer("Fade"));
        MainCamera.cullingMask = (~0) &~ (1 << LayerMask.NameToLayer("Dialogue"));
        PauseMenu.SetActive(true);
        PauseMenu.transform.position = MenuOffset.transform.position;
        PauseMenu.transform.rotation = MenuOffset.transform.rotation;
        PauseMenu.transform.rotation = Quaternion.Euler(new Vector3(MenuOffset.transform.rotation.eulerAngles.x, MenuOffset.transform.rotation.eulerAngles.y, 0));

        if (Controls!=null) Controls.SwitchLocomotion(1);
        FindObjectOfType<AutoHandPlayer>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<UnscaleMove>().Pause();
    }

    public void AppUnpause()
    {
        if (ScenarioCompleted)
        {
            return;
        }
        ForceUnpause();
    }

    public void ForceUnpause()
    {
        foreach (AudioSource Audio in AudiosWerePlaying)
        {
            Audio.UnPause();
        }
        foreach (GameObject _obj in TurnOffOnPause)
        {
            _obj.SetActive(true);
        }
        /*
        foreach (Animation Animation in AnimationsWerePlaying)
        {
            Animation.Play();
        }
        */

        PauseMenu.SetActive(false);
        if (Controls != null) Controls.SwitchLocomotion(PlayerPrefs.GetInt("MovementType", 0));

        MainCamera.cullingMask = ~0;
        UICamera.cullingMask = (1 << LayerMask.NameToLayer("Fade"));
        UICamera.gameObject.GetComponent<Camera>().enabled=false;
        GetComponent<UnscaleMove>().Play();
    }

    public void PauseSwitch()
    {
        if (!PauseMenu.activeSelf)
        {
            AppPause();
        }
        else
        {
            AppUnpause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
