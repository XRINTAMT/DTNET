using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstVariantDialogueText : MonoBehaviour
{  
    GameObject prefabVariant;
    GameObject Obj;
    DialogueEditor dialogueEditor;
    ListTextElementCustom listTextElementCustom;

    public NodeController nodeController;
    public int indexText;
    public Text TextVariant;
   
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueEditor=FindObjectOfType<DialogueEditor>();
        if (dialogueEditor != null) prefabVariant = dialogueEditor.TextDialoguePref;
        nodeController = transform.parent.gameObject.GetComponent<NodeController>();


        listTextElementCustom = new ListTextElementCustom (null,TextVariant, 0 ,0, 0);

        dialogueEditor.listDialogue[dialogueEditor.indexDialogue].listNode[nodeController.indexNode].listText.Add(listTextElementCustom);
        indexText = dialogueEditor.listDialogue[dialogueEditor.indexDialogue].listNode[nodeController.indexNode].listText.IndexOf(listTextElementCustom);
       
        listTextElementCustom.NameText = "Text " + indexText;
        listTextElementCustom.TextDialogue = TextVariant.text;
        listTextElementCustom.IndexNode = nodeController.indexNode;
        listTextElementCustom.IndexText = indexText;
        listTextElementCustom.ConnectIndexNode = -1;
    }


    public void ButtonInstVariant(Transform instPos)
    {
        Obj = Instantiate(prefabVariant, instPos.position, Quaternion.identity);

        Obj.transform.parent = transform.parent;
        Obj.transform.localScale = new Vector3(1, 1, 1);

    
    }
    public void ButtonDelVariant()
    {
        dialogueEditor.listDialogue[dialogueEditor.indexDialogue].listNode[nodeController.indexNode].listText.Remove(listTextElementCustom);
        Destroy(gameObject);
    }
    // Update is called once per frame
    public void SetConnection(int index) 
    {
        listTextElementCustom.ConnectIndexNode = index;
    }
    public void InputText() 
    {
        listTextElementCustom.TextDialogue = TextVariant.text;
    }
    void Update()
    {
        InputText();
    }
}
