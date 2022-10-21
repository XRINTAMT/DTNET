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
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ButtonInstNode() 
    {
        GameObject instNode;
        instNode = Instantiate(NodePref, NodePref.transform.position, Quaternion.identity);
    }
    public void ButtonInstVariant(Transform instPos)
    {
        GameObject instVariant;
        instVariant = Instantiate(VariantPref, instPos.position, Quaternion.identity);

        instVariant.transform.parent = parent;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
