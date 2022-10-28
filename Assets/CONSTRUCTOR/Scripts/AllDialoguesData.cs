using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TextListNode
{
    public List<string> Text = new List<string>();
}


public class AllDialoguesData : MonoBehaviour
{
    TextListNode textList = new TextListNode();


    public List<NodeList> Dialogues = new List<NodeList>();

    public int nodes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonCreateNode() 
    {
    
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
