using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstVariantDialogueText : MonoBehaviour
{
    [SerializeField] GameObject prefabVariant;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ButtonInstVariant(Transform instPos)
    {
        Debug.Log(66);
        GameObject instVariant;
        instVariant = Instantiate(prefabVariant, instPos.position, Quaternion.identity);

        instVariant.transform.parent = transform.parent;
        instVariant.transform.localScale = new Vector3(1, 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
