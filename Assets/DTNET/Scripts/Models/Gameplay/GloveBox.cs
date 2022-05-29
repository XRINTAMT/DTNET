using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models {
    public class GloveBox : MonoBehaviour
    {
        public TaskSystem taskSystem;

        public void SelectGloveBox() {
            taskSystem.PutOnGloves();
        }
    }
}

