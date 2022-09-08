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
    public TeleportHand teleportHand;
    public MoveHand moveHand;
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
        language = (Language)UIController.language;
        teleportHand = (TeleportHand)UIController.teleportLeftHand;
        moveHand = (MoveHand)UIController.moveLeftHand;
        subtitles = (Subtitles)UIController.subtitles;
        guides = (Guide)UIController.guides;
    }
}


public enum Language { English, German, Lithuanian }
public enum TeleportHand { RightHand, LeftHand }
public enum MoveHand { leftHand, RightHand }
public enum Subtitles { Enable, Disable }
public enum Guide { Enable, Disable }

