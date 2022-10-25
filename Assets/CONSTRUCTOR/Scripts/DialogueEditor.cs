using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditor : MonoBehaviour
{
    [SerializeField] GameObject NodePref;
    [SerializeField] GameObject VariantPref;
    [SerializeField] Transform PointInstVariantPref;
    [SerializeField] Transform parent;

    public static bool moveObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ButtonInstNode() 
    {
        GameObject instNode;
        instNode = Instantiate(NodePref, NodePref.transform.position, Quaternion.identity);
        instNode.transform.parent = parent;
        instNode.transform.localScale = new Vector3(1,1,1);
        instNode.transform.position = new Vector3(0, -50, 0);
    }
    public void ButtonInstVariant(Transform instPos)
    {
        GameObject instVariant;
        instVariant = Instantiate(VariantPref, instPos.position, Quaternion.identity);

        //instVariant.transform.parent = parent;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
