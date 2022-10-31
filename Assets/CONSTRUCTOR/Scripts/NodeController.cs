using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    public int indexNode;

    public Text character; 

    [SerializeField] float OffsetMoveX;
    [SerializeField] float OffsetMoveY;
    [SerializeField] float OffsetMoveZ;

    bool startFollow;
    DialogueEditor dialogueEditor;
    public List<GameObject> TextVariant = new List<GameObject>();

    private void Start()
    {
        dialogueEditor = FindObjectOfType<DialogueEditor>();
    }


    public void PointDown()
    {
        startFollow = true;
    }
    public void PointUp()
    {
        startFollow = false;
    }

    public void ButtonDelNode() 
    {
        for (int i = 0; i < TextVariant.Count; i++)
        {
            TextVariant[i].GetComponent<InstVariantDialogueText>().NodeConnector.GetComponent<ConnectionControllerNode>().DeleteConnection();
        }

        dialogueEditor.ButtonDelNode(indexNode);
        Destroy(gameObject);

    
    }
    public void InputCharacter()
    {
        dialogueEditor.listDialogue[dialogueEditor.indexDialogue].listNode[indexNode].Character = character.text;
    }


    void Update()
    {

        InputCharacter();

        if (startFollow)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + +OffsetMoveX, Input.mousePosition.y + OffsetMoveY, Camera.main.transform.position.y + OffsetMoveZ));
            transform.position = mousePosition;
        }
    }
}
