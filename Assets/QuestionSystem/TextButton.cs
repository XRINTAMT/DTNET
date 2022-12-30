using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestionSystem
{
    public class TextButton : MonoBehaviour
    {
        [SerializeField] Text NormalText;
        [SerializeField] Text SelectedText;
        [SerializeField] Text PressedText;
        [SerializeField] QuestionDialogueManager QDManager;
        Question question;

        void Start()
        {

        }

        public void Refresh(Question _question)
        {
            question = _question;
            if(_question == null)
            {
                Debug.Log("im null");
                NormalText.text = "";
                SelectedText.text = "";
                PressedText.text = "";
                return;
            }
            else
            {
                Debug.Log("im not null");
                string Language = PlayerPrefs.GetString("Language", "English");
                NormalText.text = _question.Text[Language];
                SelectedText.text = _question.Text[Language];
                PressedText.text = _question.Text[Language];
            }
        }

        public void Submit()
        {
            if(question != null)
                QDManager.Ask(question);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

