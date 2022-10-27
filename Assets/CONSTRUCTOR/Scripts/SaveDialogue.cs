using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TextList
{
    public List<string> Text;
}
[System.Serializable]
public class NodeList
{
    public List<TextList> Nodes;
}

public class SaveDialogue : MonoBehaviour
{

    public List<NodeList> Dialogues;
 
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
