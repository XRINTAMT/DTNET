using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalerController : MonoBehaviour
{
    public float delay;
    public float delayChangeValueOnMonitor;
    public float delayStartDialogue;
    public float speed;
    public float palerValue;
    Material mat;
    public bool startPaler;
    float valueColor;
    Color color;
    public VitalsMonitor vitalsMonitor;
    AudioSource audioSource;
    DialogueSystem dialogueSystem;
    // Start is called before the first frame update
    void Awake()
    {
        mat = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
        dialogueSystem = FindObjectOfType<DialogueSystem>(true);
    }
    public void AvtivatePaler() 
    {
        Invoke("StartPaler", delay);
    }
    public void StartPaler()
    {
        startPaler = true;
    }
    public void ChangeValue()
    {
        vitalsMonitor.ChangeValueInspector("0, 24, 10"); //HR
        vitalsMonitor.ChangeValueInspector("2, 40, 0"); //BP
        vitalsMonitor.ChangeValueInspector("3, 0, 0"); //BP
        vitalsMonitor.ChangeValueInspector("4, 80, 80"); //SO2
        vitalsMonitor.ChangeValueInspector("5, 34, 34"); //RespRate
    }

    public void StartDialogue() 
    {
        dialogueSystem.ActivateDialogue(3);
    }
    // Update is called once per frame
    void Update()
    {
        if (startPaler && valueColor<palerValue)
        {
            valueColor += speed * Time.deltaTime;
            color = new Color(valueColor, valueColor, valueColor);
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", color);

        }
        if (startPaler && valueColor > palerValue)
        {
            mat.EnableKeyword("_EMISSION");
            startPaler = false;
            Invoke("ChangeValue", delayChangeValueOnMonitor);
            Invoke("StartDialogue", delayStartDialogue);
            audioSource.Play();
        }
    }
}
