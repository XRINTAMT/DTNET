using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DTNET.Models {
    public class Sink : MonoBehaviour
    {

        public TaskSystem taskSystem;

        public void WasSelected() {
            taskSystem.SinkUsed();
        }

    }
}


