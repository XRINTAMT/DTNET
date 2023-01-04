using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRaycastTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<UnityEngine.UI.Graphic>().raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
