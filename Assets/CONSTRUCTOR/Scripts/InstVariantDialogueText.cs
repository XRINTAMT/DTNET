using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstVariantDialogueText : MonoBehaviour
{
    GameObject prefabVariant;
    GameObject Obj;
    DialogueEditor dialogueEditor;
    // Start is called before the first frame update
    void Start()
    {
        dialogueEditor=FindObjectOfType<DialogueEditor>();
        if (dialogueEditor != null) prefabVariant = dialogueEditor.TextDialoguePref;
    }
    public void ButtonInstVariant(Transform instPos)
    {
        Obj = Instantiate(prefabVariant, instPos.position, Quaternion.identity);

        Obj.transform.parent = transform.parent;
        Obj.transform.localScale = new Vector3(1, 1, 1);
    }
    public void ButtonDelVariant()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
