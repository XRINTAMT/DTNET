using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstVariantDialogueText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ButtonInstVariant(Transform instPos)
    {
        GameObject instVariant;
        instVariant = Instantiate(gameObject, instPos.position, Quaternion.identity);

        instVariant.transform.parent = transform.parent;
        instVariant.transform.localScale = new Vector3(1, 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
