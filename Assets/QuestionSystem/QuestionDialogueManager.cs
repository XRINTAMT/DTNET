using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace QuestionSystem
{
    [ExecuteInEditMode]
    public class QuestionDialogueManager : MonoBehaviour
    {
        [SerializeField] string DialogueName;
        [SerializeField] List<string> unlockedTopics;
        [SerializeField] TopicTabsManager Tabs;
        [SerializeField] ChoiceMenu CMenu;
        [SerializeField] DialogueLines DLines;
        [SerializeField] int mood = 0;
        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        [SerializeField] float TimeForAsking = 20;
        Coroutine QuestionTimeout;
        public bool Sync;
        

        void Start()
        {
            foreach (Question q in Dialogue)
            {
                q.GetReady();
            }
            foreach (Question q in Dialogue)
            {
                if (q.PrerequisiteTag != string.Empty)
                {
                    int index = Dialogue.IndexOf(Dialogue.FirstOrDefault(question => (string.Compare(question.Tag, q.PrerequisiteTag) == 0)));
                    q.Prerequisite = Dialogue[index];
                }
                else
                {
                    q.Prerequisite = null;
                }
            }
            Refresh();
            QuestionTimeout = StartCoroutine(WaitingForTooLong());
        }

        private void Refresh()
        {
            foreach (Question question in Dialogue)
            {
                if (!question.PrerequisiteMet())
                    continue;
                if (!unlockedTopics.Contains(question.Topic))
                    unlockedTopics.Add(question.Topic);
            }
            Tabs.Refresh(unlockedTopics);
        }

        public void ChangeTopic(string _topic)
        {
            List<Question> _questionsInTheTopic = new List<Question>();
            foreach (Question question in Dialogue)
            {
                if (question.Topic == _topic)
                {
                    _questionsInTheTopic.Add(question);
                }
            }
            CMenu.RefreshTopic(_questionsInTheTopic);
        }

        public void Ask(Question _q)
        {
            StopCoroutine(QuestionTimeout);
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.RenderQuestion(_q);
            Refresh();
        }

        public void LineCompleted(Question _q)
        {
            CMenu.gameObject.SetActive(true);
            DLines.gameObject.SetActive(false);
            QuestionTimeout = StartCoroutine(WaitingForTooLong());
            if (_q != null)
            {
                if (_q.IsAsked > 0)
                    mood -= _q.IsAsked;
                _q.IsAsked++;
                mood += _q.MoodChanges;
            }            
        }

        IEnumerator WaitingForTooLong()
        {
            for (float i = 0; i < TimeForAsking; i += Time.deltaTime)
            {
                yield return 0;
            }
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.TimeoutNotice();
            mood -= 1;
        }

        #if UNITY_EDITOR

        public void SyncDialogues()
        {
            string Path = Application.dataPath + "/Resources/Localization/"+ DialogueName +".csv";
            Dialogue = QuestionReader.ReadQuestionsFromCsv(Path);
        }

        private void OnValidate()
        {
            if(Sync == true)
            {
                Sync = false;
                SyncDialogues();
            }
        }

        #endif
    }

}
