using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Actions {
    public class VisualGuideHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void show() {
            gameObject.SetActive(true);
        }

        public void hide() {
            gameObject.SetActive(false);
        }

        public void setPosition(Vector3 pos) {
            gameObject.transform.position = pos;
        }
    }
}
