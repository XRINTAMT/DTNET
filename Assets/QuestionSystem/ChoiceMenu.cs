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
        List<Question> Questions;

        int totalPages = 1;
        int currentPage = 0;

        void Start()
        {

        }

        public void RefreshTopic(List<Question> _questions)
        {
            Questions = _questions;
            currentPage = 0;
            totalPages = (int)Mathf.Ceil(((float)Questions.Count / 3)) - 1;
            
        }

        private void RefreshPage()
        {
            for (int i = 0; i < textButton.Length; i++)
            {
                if (currentPage * 3 + i > Questions.Count)
                    textButton[i].Refresh(null);
                textButton[i].Refresh(Questions[currentPage * 3 + i]);
            }
            ArrowLeft.SetActive(currentPage == 0);
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
    
