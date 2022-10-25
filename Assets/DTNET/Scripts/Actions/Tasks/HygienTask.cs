using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.Actions;
using DTNET.Handlers;

namespace DTNET.Tasks {

    [RequireComponent(typeof(AudioSource))]
    public class HygienTask : MonoBehaviour
    {
        public TaskTrackingHandler taskTracking;
        public CameraUIHandler cameraUI;

        private AudioSource audioData;
        private bool hasNotBeenSelected = true;

        void Start()
        {
            audioData = GetComponent<AudioSource>();
        }

        public void PerformHygienTask() {
            taskTracking.HygienTaskCompleted();
            audioData.Play(0);
            Debug.Log("PerformHygienTask!");
            if(hasNotBeenSelected) {
                cameraUI.DisplayMessage("Next\nAsk Patient for ID");
                hasNotBeenSelected = false;
                //visualGuide.hide();
            }
        }
    }
}
