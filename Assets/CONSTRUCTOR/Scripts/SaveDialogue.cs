using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TextList
{
    public List<string> Text = new List<string>();
}
[System.Serializable]
public class NodeList
{
    public List<TextList> Nodes = new List<TextList>();
}

public class SaveDialogue : MonoBehaviour
{
    TextList textList = new TextList();
    NodeList nodeList = new NodeList();

    public int countNodes;

    public List<NodeList> Dialogues = new List<NodeList>();



    //ClickObject obj =new ClickObject();
    //public List<ClickObject> te = new List<ClickObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonSaveDialogue() 
    {
        //te.Add(obj);

        Dialogues.Add(new NodeList());
    
        //for (int i = 0; i < countNodes; i++)
        //{
        //    nodeList.Nodes.Add(textList);
        //}
       

    
    
    }
}
