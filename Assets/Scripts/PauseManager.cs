using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject MenuOffset;
    [SerializeField] GameObject Congrats;
    [SerializeField] GameObject Main;
    [SerializeField] Camera UICamera;
    [SerializeField] Camera MainCamera;
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

    public void ShowOutroMessage()
    {
        AppPause();
        ScenarioCompleted = true;
        Congrats.SetActive(true);
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
        UICamera.cullingMask = (1 << LayerMask.NameToLayer("Hand")) | (1 << LayerMask.NameToLayer("MenusUI")) | (1 << LayerMask.NameToLayer("Fade"));
        MainCamera.cullingMask = (~0) &~ (1 << LayerMask.NameToLayer("Dialogue"));
        PauseMenu.SetActive(true);
        PauseMenu.transform.position = MenuOffset.transform.position;
        PauseMenu.transform.rotation = MenuOffset.transform.rotation;
        PauseMenu.transform.rotation = Quaternion.Euler(new Vector3(MenuOffset.transform.rotation.eulerAngles.x, MenuOffset.transform.rotation.eulerAngles.y, 0));
        Controls.SwitchLocomotion(-1);
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
        /*
        foreach (Animation Animation in AnimationsWerePlaying)
        {
            Animation.Play();
        }
        */
        PauseMenu.SetActive(false);
        Controls.SwitchLocomotion(PlayerPrefs.GetInt("MovementType", 0));
        MainCamera.cullingMask = ~0;
        UICamera.cullingMask = (1 << LayerMask.NameToLayer("Fade"));
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
