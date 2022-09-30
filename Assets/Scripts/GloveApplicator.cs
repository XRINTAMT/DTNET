using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveApplicator : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] Hands;
    [SerializeField] Material GloveMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Apply()
    {
        for(int i = 0; i < Hands.Length; i++)
        {
            Hands[i].material = GloveMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
