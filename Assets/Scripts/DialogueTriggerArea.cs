using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour
{
    public bool inArea;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject dialogueIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InArea()
    {
        inArea = true;
    }
    public void OutArea()
    {
        inArea = false;
    }
    public void activateDialogue() 
    {
        if (inArea)
        {
            dialogue.SetActive(true);
            dialogueIcon.SetActive(false);
        }
    }
   
}
