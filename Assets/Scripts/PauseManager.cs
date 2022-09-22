using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject MenuOffset;
    List<AudioSource> AudiosWerePlaying;
    List<Animation> AnimationsWerePlaying;
    XRMovementControls Controls;
    void Awake()
    {
        AudiosWerePlaying = new List<AudioSource>();
        AnimationsWerePlaying = new List<Animation>();
        Controls = FindObjectOfType<XRMovementControls>();
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
        PauseMenu.SetActive(true);
        PauseMenu.transform.position = MenuOffset.transform.position;
        PauseMenu.transform.rotation = MenuOffset.transform.rotation;
        PauseMenu.transform.rotation = Quaternion.Euler(new Vector3(MenuOffset.transform.rotation.eulerAngles.x, MenuOffset.transform.rotation.eulerAngles.y, 0));
        Controls.SwitchLocomotion(-1);
    }

    public void AppUnpause()
    {
        foreach(AudioSource Audio in AudiosWerePlaying)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
