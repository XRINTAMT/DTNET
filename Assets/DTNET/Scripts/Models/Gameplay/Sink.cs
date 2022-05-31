using UnityEngine;

namespace DTNET.Models {
    [RequireComponent(typeof(AudioSource))]
    public class Sink : MonoBehaviour
    {
        public TaskSystem taskSystem;
        private AudioSource audioData;

        void Start()
        {
            audioData = GetComponent<AudioSource>();
        }

        public void WasSelected() {
            taskSystem.SinkUsed();
            audioData.Play(0);
        }

    }
}
