using UnityEngine;

using DTNET.UI;

namespace DTNET.Models {
    [RequireComponent(typeof(AudioSource))]
    public class Sink : MonoBehaviour
    {
        public TaskSystem taskSystem;

        public CameraUI cameraUI;
        private AudioSource audioData;

        private bool hasNotBeenSelected = true;


        void Start()
        {
            audioData = GetComponent<AudioSource>();
        }

        public void WasSelected() {
            taskSystem.SinkUsed();
            audioData.Play(0);

            if(hasNotBeenSelected) {
                cameraUI.DisplayMessage("Now Ask for ID");
                hasNotBeenSelected = false;
            }
        }

    }
}
