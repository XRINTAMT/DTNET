using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechPatient : MonoBehaviour
{
    [SerializeField] private GameObject answer;

    [SerializeField] private List <Text> textAnswer;
    [SerializeField] private List <AudioClip> audioClip;
    [SerializeField] private List <bool> stageAnswer;

    float audioLenght;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.GetComponent<AudioSource>();
    }

    public void StartConversationWithDoctor(int stage) 
    {
        answer.SetActive(true);
        answer.GetComponent<TextMeshPro>().text = textAnswer[stage].text;
        audioSource.clip = audioClip[stage];
        audioLenght = audioClip[stage].length;
        StartCoroutine(EndAnswer());
    }
    private IEnumerator EndAnswer() 
    {
        answer.SetActive(false);
        yield return new WaitForSeconds(audioLenght);
    
    }
}
