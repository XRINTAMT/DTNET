using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocalizedAudioPlayer : MonoBehaviour
{
    private AudioSource AS;
    public Action<int, string> playPhrase;
    [System.Serializable]
    class LanguagePhraseSet{
        [field:SerializeField] public string Language { get; private set; }
        [field: SerializeField] public AudioClip[] Phrase { get; private set; }
    }

    [SerializeField] LanguagePhraseSet[] Languages;

    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayPhrase(int id)
    {
        Debug.Log("Playing " + id + " on " + gameObject.name);
        string lang = PlayerPrefs.GetString("Language", "English");
        for (int i = 0; i < Languages.Length; i++)
        {
            if (Languages[i].Language == lang)
            {
                if(id < Languages[i].Phrase.Length)
                {
                    AS.clip = Languages[i].Phrase[id];
                    AS.Play();
                }
                else
                {
                    Debug.LogWarning("Phrase number " + id + " is not defined for " + lang + " language");
                }
            }

        }
        if (!PhotonManager.offlineMode)
        {
            playPhrase?.Invoke(id, lang);
        }
    }

    public void PlayPhrasePhoton(int id, string language)   //for multiplayer
    {
        for (int i = 0; i < Languages.Length; i++)
        {
            if (Languages[i].Language == language)
            {
                if (id < Languages[i].Phrase.Length)
                {
                    AS.clip = Languages[i].Phrase[id];
                    AS.Play();
                }
                else
                {
                    Debug.LogWarning("Phrase number " + id + " is not defined for " + language + " language");
                }
            }
        }
    }
}
