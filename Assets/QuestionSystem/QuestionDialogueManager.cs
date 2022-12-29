using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    [ExecuteInEditMode]
    public class QuestionDialogueManager : MonoBehaviour
    {
        [SerializeField] string DialogueName;
        [SerializeField] List<string> unlockedTopics;
        [SerializeField] TopicTabsManager Tabs;
        [SerializeField] ChoiceMenu CMenu;
        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        public bool Sync;

        void Start()
        {
            Refresh();
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
