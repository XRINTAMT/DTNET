using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ListTextElementCustom
{ 
   
    [HideInInspector] public string NameText;
    [HideInInspector] public int IndexNode;
    public string TextDialogue;
    public int IndexText;
    public int ConnectIndexNode;        

    public ListTextElementCustom(string name,Text text,int indexNode, int indexText, int indexConnect)
    {
        NameText = name;
        TextDialogue = text.text;
        IndexNode = indexNode;
        IndexText = indexText;
        ConnectIndexNode = indexConnect;
    }
}

[System.Serializable]
public class ListText
{
    [HideInInspector] public string NameNode;
    public string Character;
    public int IndexNode;
    [HideInInspector] public GameObject Node;
    public List<ListTextElementCustom> listText = new List<ListTextElementCustom>();
    public ListText(string text, int indexNode, string character, GameObject node)
    {
        NameNode = text;
        IndexNode = indexNode;
        Character = character;
        Node = node;
    }
}
[System.Serializable]
public class ListNode
{
    [HideInInspector] public string NameDialogue;
    public int IndexDialogue;
    public List<ListText> listNode = new List<ListText>();

    public ListNode(string text, int indexDialogue)
    {
        NameDialogue = text;
        IndexDialogue = indexDialogue;
    }
}



public class DialogueEditor : MonoBehaviour
{
    [SerializeField] GameObject NodePref;
    public GameObject TextDialoguePref;
    public int indexDialogue;
    public Text nameDialogue;

    public List<ListNode> listDialogue = new List<ListNode>();

    void Start()
    {
        
    }

    public void ButtonCreateDialogue() 
    {
        ListNode listNode = new ListNode(null,0);
        listDialogue.Add(listNode);
        indexDialogue = listDialogue.IndexOf(listNode);
        listDialogue[indexDialogue].NameDialogue = nameDialogue.text;
        listDialogue[indexDialogue].IndexDialogue = indexDialogue;
    }
    public void ButtonInstNode() 
    {
        GameObject instNode;
        instNode = Instantiate(NodePref, NodePref.transform.position, Quaternion.identity);
        instNode.transform.parent = transform;
        instNode.transform.localScale = new Vector3(1,1,1);
        instNode.transform.localPosition = new Vector3(0, 0, 0);

 
        ListText listNode = new ListText(null,0, null,null);
        listDialogue[indexDialogue].listNode.Add(listNode);

        int index = listDialogue[indexDialogue].listNode.IndexOf(listNode);
        instNode.GetComponent<NodeController>().indexNode = index;
        //instNode.GetComponent<NodeController>().indexTextNode.text = "" + index;

        listDialogue[indexDialogue].listNode[index].NameNode = "Node " + index;
        listDialogue[indexDialogue].listNode[index].IndexNode = index;
        listDialogue[indexDialogue].listNode[index].Character = instNode.GetComponent<NodeController>().character.text;
        listDialogue[indexDialogue].listNode[index].Node = instNode;

    }
    public void ButtonDelNode(int index)
    {
        listDialogue[indexDialogue].listNode.Remove(listDialogue[indexDialogue].listNode[index]);
        for (int i = 0; i < listDialogue[indexDialogue].listNode.Count; i++)
        {

            int indexChange = listDialogue[indexDialogue].listNode.IndexOf(listDialogue[indexDialogue].listNode[i]);
            listDialogue[indexDialogue].listNode[i].Node.GetComponent<NodeController>().indexNode = indexChange;
            listDialogue[indexDialogue].listNode[i].NameNode = "Node " + indexChange;
            listDialogue[indexDialogue].listNode[i].IndexNode = indexChange;
        }
    }

    void Update()
    {
        
    }
}
