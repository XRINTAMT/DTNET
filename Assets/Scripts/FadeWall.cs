using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWall : MonoBehaviour
{

    FadeMessageManager fadeMessageManager;
    // Start is called before the first frame update
    void Start()
    {
        fadeMessageManager=FindObjectOfType<FadeMessageManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall") 
            fadeMessageManager.FadeWallTrue();
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall")
            fadeMessageManager.FadeWallFalse();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
