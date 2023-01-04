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
        [SerializeField] List<string> totalInformation;
        [SerializeField] List<string> unlockedInformation;
        [SerializeField] GameObject PatientObject;
        [SerializeField] TopicTabsManager Tabs;
        [SerializeField] ChoiceMenu CMenu;
        [SerializeField] DialogueLines DLines;
        [SerializeField] int mood = 0;
        [SerializeField] int questionsLimit = 20;
        [SerializeField] AudioSource NurseSource;
        
        AudioSource PatientSource;
        FaceAnimationController FAController;

        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        [SerializeField] float TimeForAsking = 20;
        Coroutine QuestionTimeout;
        public bool Sync;
        int questionsCount = 0;


        void Start()
        {
            PatientSource = PatientObject.GetComponent<AudioSource>();
            FAController = PatientObject.GetComponent<FaceAnimationController>();
            totalInformation = new List<string>();
            unlockedInformation = new List<string>();
            foreach (Question _q in Dialogue)
            {
                _q.GetReady();
                if (!totalInformation.Contains(_q.InformationTag))
                {
                    totalInformation.Add(_q.InformationTag);
                }
            }
            foreach (Question _q in Dialogue)
            {
                if (_q.PrerequisiteTag != string.Empty)
                {
                    int index = Dialogue.IndexOf(Dialogue.FirstOrDefault(question => (string.Compare(question.Tag, _q.PrerequisiteTag) == 0)));
                    _q.Prerequisite = Dialogue[index];
                }
                else
                {
                    _q.Prerequisite = null;
                }
            }
            Refresh();
            ChangeTopic("Introduction");
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
            Tabs.RefreshTopic(_topic);
        }

        public void Ask(Question _q)
        {
            StopCoroutine(QuestionTimeout);
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.RenderQuestion(_q);
            Refresh();
        }

        public void ProcessAnswer(Question _q)
        {
            if (_q != null)
            {
                if (!unlockedInformation.Contains(_q.InformationTag))
                {
                    unlockedInformation.Add(_q.InformationTag);
                    Debug.Log("Information discovered: " + unlockedInformation.Count + "/" + totalInformation.Count());
                }
                if (_q.IsAsked > 0)
                    mood -= _q.IsAsked;
                _q.IsAsked++;
                mood += _q.MoodChanges;
                FAController.SetMoodIndex(100 + (mood * 2));
                //FAController.SetMood(mood);
                //need a method SetMood(mood) in FaceAnimationController that would
                //set the mood to this value (optionally with an overshoot)

                //load the audio for the patient here
                PatientSource.Play();
            }

        }

        public void LineCompleted(Question _q)
        {
            CMenu.gameObject.SetActive(true);
            DLines.gameObject.SetActive(false);
            QuestionTimeout = StartCoroutine(WaitingForTooLong());
            
            questionsCount++;
            if(questionsCount == questionsLimit)
            {
                Debug.Log("Call a method on a results screen object");
                //Call a method on a results screen object
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
