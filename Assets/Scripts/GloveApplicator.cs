using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GloveApplicator : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] Hands;
    public Material GloveMaterial;
    public Action apply;
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

        apply?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
