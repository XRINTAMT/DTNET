using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeParent : MonoBehaviour
{
    Transform grandpa;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp)
        {
            grandpa = transform.parent.parent;
            Debug.Log(grandpa.gameObject.name);
        }
        if (PhotonManager._viewerApp)
        {
            Destroy(GetComponent<Joint>());
        }

    }

    public void Escape()
    {
        if (!PhotonManager._viewerApp)
        {
            transform.SetParent(grandpa);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
