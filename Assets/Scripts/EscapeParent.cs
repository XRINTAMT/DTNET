using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeParent : MonoBehaviour
{
    Transform grandpa;
    // Start is called before the first frame update
    void Start()
    {
        grandpa = transform.parent.parent;
        Debug.Log(grandpa.gameObject.name);
    }

    public void Escape()
    {
        transform.SetParent(grandpa);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
