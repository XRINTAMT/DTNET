using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List <AudioClip> clips;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip clip) 
    {
        audioSource.clip = clip;
        audioSource.Play();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
