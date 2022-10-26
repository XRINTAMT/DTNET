using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditor : MonoBehaviour
{
    [SerializeField] GameObject NodePref;
    public GameObject TextDialoguePref;

    public static bool moveObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ButtonInstNode() 
    {
        GameObject instNode;
        instNode = Instantiate(NodePref, NodePref.transform.position, Quaternion.identity);
        instNode.transform.parent = transform;
        instNode.transform.localScale = new Vector3(1,1,1);
        instNode.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        
    }
}
