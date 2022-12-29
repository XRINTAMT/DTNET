using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem
{
    public class TabButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Image[] Icon;
        [SerializeField] QuestionDialogueManager QDManager;
        string buttonTopic;
        // Start is called before the first frame update
        void Awake()
        {
            buttonTopic = null;
        }

        public void Refresh(string _topic)
        {
            buttonTopic = _topic; 
            if (_topic == null)
            {
                foreach (Image image in Icon)
                {
                    image.enabled = false;
                }
                return;
            }
            Sprite currentSprite = Resources.Load<Sprite>("TopicIcons/" + _topic);
            foreach (Image image in Icon)
            {
                image.enabled = true;
                image.sprite = currentSprite;
            }
        }

        public void Submit()
        {
            if(buttonTopic != null)
            {
                QDManager.ChangeTopic(buttonTopic);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
