using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;

namespace QuestionSystem
{
    public class DialogueLines : MonoBehaviour
    {
        [SerializeField] Text Line;
        [SerializeField] QuestionDialogueManager QDManager;
        Question q;
        bool answer = true;

        void Start()
        {

        }

        public void RenderQuestion(Question _q)
        {
            q = _q;
            string Language = PlayerPrefs.GetString("Language", "English");
            Line.text = "You:\n" + q.Text[Language];
        }

        public void GoOn()
        {
            if (answer == true)
            {
                answer = false;
                QDManager.ProcessAnswer(q);
                string Language = PlayerPrefs.GetString("Language", "English");
                if (q.IsAsked < 2)
                {
                    Line.text = "Patient:\n" + q.Answer[Language];
                }
                else
                {
                    Line.text = "Patient:\n" + LocalizationManager.Localize("AlreadyToldYou") + "\n" + q.Answer[Language];

                }

            }
            else
            {
                answer = true;
                QDManager.LineCompleted(q);
            }
        }

        public void TimeoutNotice()
        {
            answer = false;
            q = null;
            Line.text = "Patient:\n" + LocalizationManager.Localize("TakingTooLong");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
