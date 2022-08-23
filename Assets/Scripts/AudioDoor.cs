using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDoor : MonoBehaviour
{
    [SerializeField] private AudioClip openDoor;  
    [SerializeField] private AudioClip closeDoor;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OpenDoorSound() 
    {
        audioSource.clip = openDoor;
        audioSource.Play();
    }
    public void CloseDoorSound()
    {
        audioSource.clip = closeDoor;
        audioSource.Play();
    }

}
