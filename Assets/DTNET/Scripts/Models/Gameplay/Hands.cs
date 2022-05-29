using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models {
    public class Hands : MonoBehaviour
    {
        private Renderer rend;
        void Start()
        {
            rend = GetComponent<Renderer>();
        }

        void PutOnGloves(Collider other) {
            if(other.gameObject.name == "GloveBox") 
            {
                //Change color on hand prefabs
                rend.material.color = Color.red;
            }
        }
    }
}
