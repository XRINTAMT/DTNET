using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    public class ChoiceMenu : MonoBehaviour
    {
        [SerializeField] GameObject ArrowLeft;
        [SerializeField] GameObject ArrowRight;
        [SerializeField] TextButton[] textButton;
        QuestionDialogueManager dialogue;
        List<Question> questions;

        int totalPages = 1;
        int currentPage = 0;

        void Start()
        {

        }

        public void InjectDialogue(QuestionDialogueManager _dialogue)
        {
            dialogue = _dialogue;
        }

        public void RefreshTopic(List<Question> _questions, bool newTopic)
        {
            questions = _questions;
            currentPage = newTopic ? 0 : currentPage;
            totalPages = (int)Mathf.Ceil(((float)questions.Count / 3)) - 1;
            RefreshPage();
        }

        private void RefreshPage()
        {
            for (int i = 0; i < textButton.Length; i++)
            {
                int currentText = currentPage * 3 + i;
                if (currentText >= questions.Count)
                    textButton[i].Refresh(null);
                else
                    textButton[i].Refresh(questions[currentText]);
            }
            ArrowLeft.SetActive(currentPage != 0);
            ArrowRight.SetActive(currentPage < totalPages);
        }

        public void FlipPage(int delta)
        {
            currentPage = currentPage + delta;
            RefreshPage();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
    
