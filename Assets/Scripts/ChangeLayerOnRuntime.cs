using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnRuntime : MonoBehaviour
{
    [SerializeField] string layer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
