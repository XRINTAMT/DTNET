using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingAnimation : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public AudioPeer audioPeer;
    public int frequencyBand = 0;
    public float multiplier = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (audioPeer == null) return;
   
        skinnedMeshRenderer.SetBlendShapeWeight(67, audioPeer.GetFrequencyBand(frequencyBand) * multiplier);
    }
}
