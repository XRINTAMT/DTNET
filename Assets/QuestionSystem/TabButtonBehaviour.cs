using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;

namespace QuestionSystem
{
    public class TabButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Image[] Icon;
        [SerializeField] LocalizedText TextClarification;
        [SerializeField] QuestionDialogueManager QDManager;
        [SerializeField] GameObject TextContainer;
        public string buttonTopic { get; private set; }
        
        void Awake()
        {
            buttonTopic = null;
        }

        public void Refresh(string _topic)
        {
            buttonTopic = _topic; 
            if (_topic == null || _topic == "null")
            {
                foreach (Image image in Icon)
                {
                    image.enabled = false;
                }
                TextContainer.SetActive(false);
                return;
            }
            Sprite currentSprite = Resources.Load<Sprite>("TopicIcons/" + _topic);
            TextContainer.SetActive(true);
            TextClarification.LocalizationKey = _topic;
            TextClarification.Localize();
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

        public void Activate(bool _active)
        {
            Icon[2].gameObject.SetActive(_active);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
