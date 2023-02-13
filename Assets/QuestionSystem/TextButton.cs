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
        [SerializeField] GameObject FullTextWindow;
        [SerializeField] Text FullText;
        [SerializeField] QuestionDialogueManager QDManager;
        Question question;
        bool isNew;

        void Start()
        {

        }

        public void Refresh(Question _question)
        {
            question = _question;
            if(question == null)
            {
                NormalText.text = "";
                SelectedText.text = "";
                PressedText.text = "";
                return;
            }
            else
            {
                string Language = PlayerPrefs.GetString("Language", "English");
                string _short = ((question.isNew) ? "[!] " : "") + question.Short[Language];
                NormalText.text = _short;
                SelectedText.text = _short;
                PressedText.text = _short;
                FullText.text = question.Text[Language];
            }
        }

        public void OnHover()
        {
            if (question == null)
                return;
            string Language = PlayerPrefs.GetString("Language", "English");
            if (question.Text[Language] != question.Short[Language])
            {
                //Not showing that for now
                //FullTextWindow.SetActive(true);
            }
            if (question.isNew)
            {
                isNew = false;
                string _short = question.Short[Language];
                NormalText.text = _short;
                SelectedText.text = _short;
                PressedText.text = _short;
                QDManager.SeenQuestion(question);
            }
        }

        public void Submit()
        {
            FullTextWindow.SetActive(false);
            if (question != null)
            {
                QDManager.Ask(question);
            }
                
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

