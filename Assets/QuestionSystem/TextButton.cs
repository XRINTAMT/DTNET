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

        void Start()
        {

        }

        public void Refresh(Question _question)
        {
            string Language = PlayerPrefs.GetString("Language", "English");
            NormalText.text = _question.Text[Language];
            SelectedText.text = _question.Text[Language];
            PressedText.text = _question.Text[Language];

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

