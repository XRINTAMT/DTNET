using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnTrigger : MonoBehaviour
{
    [SerializeField] string layer;
    string initLayer;
    void Start()
    {
        initLayer = LayerMask.LayerToName(gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToNew()
    {
        Debug.Log("TriggerEnter");
        SetLayerRecursively(gameObject, LayerMask.NameToLayer(layer));
        
    }



    public void SwitchBack()
    {
        Debug.Log("TriggerEnter");
        SetLayerRecursively(gameObject, LayerMask.NameToLayer(initLayer));
    }


    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
