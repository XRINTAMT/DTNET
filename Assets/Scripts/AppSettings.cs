using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSettings : MonoBehaviour
{
    [Range(0f, 1f)]
    public float dialogueVolume;
    [Range(0f, 1f)]
    public float soundVolume;
    public Language language;
    public Locomotion locomotion;
    public Subtitles subtitles;
    public Guide guides;

    void Start()
    {
        DontDestroyOnLoad(this);
    }


    public void UpdateSettings()
    {
        dialogueVolume = UIController.dialogueVolume;
        soundVolume = UIController.soundVolume;
        locomotion = (Locomotion)UIController.teleport;
        subtitles = (Subtitles)UIController.subtitles;
        guides = (Guide)UIController.guides;
    }
}


public enum Language { English, German, Lithuanian }
public enum Locomotion { Teleportation, Smooth }
public enum Subtitles { Enable, Disable }
public enum Guide { Enable, Disable }

