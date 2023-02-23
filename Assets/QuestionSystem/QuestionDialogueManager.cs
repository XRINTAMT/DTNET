using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace QuestionSystem
{
    [ExecuteInEditMode]
    public class QuestionDialogueManager : MonoBehaviour
    {
        [SerializeField] string DialogueName;
        [SerializeField] List<string> unlockedTopics;
        [SerializeField] List<string> topicsWithNew;
        [SerializeField] List<string> totalInformation;
        [SerializeField] List<string> unlockedInformation;
        [SerializeField] GameObject PatientObject;
        [SerializeField] TopicTabsManager Tabs;
        [SerializeField] ChoiceMenu CMenu;
        [SerializeField] DialogueLines DLines;
        [SerializeField] CompletedScenario OutroScreen;
        [SerializeField] Text NameText;
        [SerializeField] Text DateOfBirthText;
        [SerializeField] Text NumberOfQuestionsText;
        [SerializeField] int mood = 0;
        [SerializeField] int questionsLimit = 20;
        [SerializeField] AudioSource NurseSource;
        [SerializeField] string Name;
        [SerializeField] string DateOfBirth;
        [SerializeField] List<Sprite> moodList;
        [SerializeField] int HappyThreshold = 0;
        [SerializeField] int SadThreshold = -5;
        [SerializeField] Image MoodIndicator;
        [SerializeField] List<Question> IrrelevantQuestions;
        [SerializeField] List<Question> GoodQuestionsAsked;
        [SerializeField] List<Question> GoodQuestionsMissed;
        [SerializeField] ReportListHandler IrrelevantQuestionsHandler;
        [SerializeField] ReportListHandler GoodQuestionsAskedHandler;
        [SerializeField] ReportListHandler GoodQuestionsMissedHandler;

        private DateTime scenarioStartTime;
        
        string NurseGender = "Female";
        AudioSource PatientSource;
        FaceAnimationController FAController;
        Animator BodyAnimator;

        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        [SerializeField] float TimeForAsking = 20;
        Coroutine QuestionTimeout;
        public bool Sync;
        int questionsCount = 0;
        Question Greeting;
        string topic;
        Coroutine lineCompleted;
        bool diabetesSeen;
        bool insulinSeen;


        private Sprite MoodSprite(int _mood)
        {
            if (_mood > HappyThreshold)
            {
                return moodList[2];
            }
            if (mood >= SadThreshold)
            {
                return moodList[1];
            }
            return moodList[0];
        }

        void Start()
        {
            NumberOfQuestionsText.text = (questionsLimit - questionsCount).ToString();
            PatientSource = PatientObject.GetComponent<AudioSource>();
            FAController = PatientObject.GetComponent<FaceAnimationController>();
            BodyAnimator = PatientObject.GetComponent<Animator>();
            totalInformation = new List<string>();
            topicsWithNew = new List<string>();
            unlockedInformation = new List<string>();
            scenarioStartTime = System.DateTime.Now;
            for (int i = 0; i < Dialogue.Count;)
            {
                Dialogue[i].GetReady();
                if (Dialogue[i].Answer["English"] == "EMPTY")
                {
                    Dialogue.RemoveAt(i);
                }
                else
                {
                    Dialogue[i].isNew = (Dialogue[i].PrerequisiteTag != string.Empty);

                    if (!totalInformation.Contains(Dialogue[i].InformationTag) && Dialogue[i].Relevance == 1)
                    {
                        totalInformation.Add(Dialogue[i].InformationTag);
                    }
                    i++;
                }
            }
            /*
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
            */
            NurseGender = PlayerPrefs.GetInt("Gender") == 0 ? "Female" : "Male";
            if (NurseGender == "Male")
            {
                //could iterate through languages but ok whatever

                Dialogue[0].Text["English"] = Dialogue[0].Text["English"].Replace("Marite", "Alexander");
                Dialogue[0].Text["German"] = Dialogue[0].Text["German"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Swedish"] = Dialogue[0].Text["Swedish"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Latvian"] = Dialogue[0].Text["Latvian"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Lithuanian"] = Dialogue[0].Text["Lithuanian"].Replace("Marite", "Alexander");

                Dialogue[0].Short["English"] = Dialogue[0].Short["English"].Replace("Marite", "Alexander");
                Dialogue[0].Short["German"] = Dialogue[0].Short["German"].Replace("Marite", "Alexander");
                Dialogue[0].Short["Swedish"] = Dialogue[0].Short["Swedish"].Replace("Marite", "Alexander");
                Dialogue[0].Short["Latvian"] = Dialogue[0].Short["Latvian"].Replace("Marite", "Alexander");
                Dialogue[0].Short["Lithuanian"] = Dialogue[0].Short["Lithuanian"].Replace("Marite", "Alexander");
            }
            CMenu.InjectDialogue(this);

            Refresh();
            ChangeTopic("introduction");
            //QuestionTimeout = StartCoroutine(WaitingForTooLong());

        }

        private void Refresh()
        {
            topicsWithNew.Clear();
            unlockedTopics.Clear();
            if (!diabetesSeen)
                topicsWithNew.Add("diabetes");
            if (!insulinSeen)
                topicsWithNew.Add("insulin");
            foreach (Question question in Dialogue)
            {
                if (!(unlockedInformation.Contains(question.PrerequisiteTag) || question.PrerequisiteTag == ""))
                    continue;
                
                if ((question.Topic == "diabetes" || question.Topic == "insulin") && !(unlockedInformation.Contains("has_diabetes")))
                    continue;
                
                if (!unlockedTopics.Contains(question.Topic))
                    unlockedTopics.Add(question.Topic);
                if (question.isNew)
                {
                    if (!topicsWithNew.Contains(question.Topic))
                        topicsWithNew.Add(question.Topic);
                }
            }
            Tabs.Refresh(unlockedTopics, topicsWithNew);
        }

        public void ChangeTopic(string _topic, bool _newTopic = true)
        {
            topic = _topic;
            if (_topic == "diabetes")
                diabetesSeen = true;
            if (_topic == "insulin")
                insulinSeen = true;
            List<Question> _questionsInTheTopic = new List<Question>();
            foreach (Question question in Dialogue)
            {
                if (question.Topic == _topic)
                {
                    _questionsInTheTopic.Add(question);
                }
            }
            CMenu.RefreshTopic(FilterQuestions(_questionsInTheTopic), _newTopic);
            Tabs.RefreshTopic(_topic);
            Refresh();
        }

        private void RefreshTopic(bool _newTopic = true)
        {
            List<Question> _questionsInTheTopic = new List<Question>();
            foreach (Question question in Dialogue)
            {
                if (question.Topic == topic)
                {
                    _questionsInTheTopic.Add(question);
                }
            }
            CMenu.RefreshTopic(FilterQuestions(_questionsInTheTopic), _newTopic);
            Tabs.RefreshTopic(topic);
        }

        private List<Question> FilterQuestions(List<Question> _questions)
        {
            List<Question> _result = new List<Question>();
            foreach (Question _question in _questions)
            {
                if (unlockedInformation.Contains(_question.PrerequisiteTag) || _question.PrerequisiteTag == "")
                {
                    _result.Add(_question);
                }
            }
            return _result;
        }

        public void Ask(Question _q)
        {
            if (QuestionTimeout != null)
                StopCoroutine(QuestionTimeout);
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.RenderQuestion(_q);
            NurseSource.clip = Resources.Load<AudioClip>("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + NurseGender + "Nurse/" + _q.Tag) as AudioClip;
            NurseSource.Play();
            Refresh();
        }

        public void ProcessAnswer(Question _q)
        {
            if (_q != null)
            {
                if (_q.Tag == "introduction")
                {
                    NameText.text = Name;
                }
                if (_q.Tag == "date_of_birth")
                {
                    DateOfBirthText.text = DateOfBirth;
                }
                if (_q.Relevance == 1)
                {
                    if (!unlockedInformation.Contains(_q.InformationTag))
                    {
                        unlockedInformation.Add(_q.InformationTag);
                        GoodQuestionsAsked.Add(_q);
                        Debug.Log("Information discovered: " + unlockedInformation.Count + "/" + totalInformation.Count());
                    }
                }
                else
                {
                    if (!IrrelevantQuestions.Contains(_q))
                    {
                        IrrelevantQuestions.Add(_q);
                        Debug.Log("Information discovered: " + unlockedInformation.Count + "/" + totalInformation.Count());
                    }
                }

                if (_q.IsAsked > 0)
                    mood -= _q.IsAsked;
                mood += _q.MoodChanges;

                MoodIndicator.sprite = MoodSprite(mood);
                FAController.SetMoodIndex(100 + (mood * 2));
                BodyAnimator.SetTrigger(_q.AnimationType);
                //FAController.SetMood(mood);
                //need a method SetMood(mood) in FaceAnimationController that would
                //set the mood to this value (optionally with an overshoot)

                //load the audio for the patient here
                PatientSource.clip = Resources.Load<AudioClip>("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + DialogueName + "/" + _q.Tag) as AudioClip;

                if (_q.IsAsked > 0)
                {
                    StartCoroutine(AnswerRepeated(PatientSource.clip));
                }
                else
                {
                    PatientSource.Play();
                }
                _q.IsAsked++;
                lineCompleted = StartCoroutine(AdvanceOnAudioPlayed(_q));
                Refresh();
            }

        }

        public void LineCompleted(Question _q)
        {
            if(lineCompleted != null)
            {
                StopCoroutine(lineCompleted);
                lineCompleted = null;
            }
            CMenu.gameObject.SetActive(true);
            DLines.gameObject.SetActive(false);
            QuestionTimeout = StartCoroutine(WaitingForTooLong());

            questionsCount++;
            NumberOfQuestionsText.text = (questionsLimit - questionsCount).ToString();
            if (questionsCount == questionsLimit || _q.Tag == "end_scenario")
            {
                EndScenario();
            }
            if(questionsCount == 1)
            {
                Greeting = Dialogue.Find(question => question.Tag == "introduction");
                Dialogue.Remove(Greeting);
            }
            RefreshTopic(false);
        }

        IEnumerator AdvanceOnAudioPlayed(Question _q)
        {
            yield return 0;
            while (PatientSource.isPlaying)
            {
                yield return 0;
            }
            for(float i = 0; i < 2; i += Time.deltaTime)
            {
                yield return 0;
            }
            Debug.Log("Advancing because the audio is over.");
            LineCompleted(_q);
        }

        public void EndScenario()
        {
            if (Greeting != null)
                Dialogue.Add(Greeting);
            DateTime endTime = DateTime.Now;
            var timeDiff = endTime.Subtract(scenarioStartTime);
            OutroScreen.SetData(MoodSprite(mood), totalInformation.Count, unlockedInformation.Count, DialogueName, "", "", timeDiff.TotalMinutes);
            gameObject.SetActive(false);
            IrrelevantQuestionsHandler.Initialize(IrrelevantQuestions);
            GoodQuestionsAskedHandler.Initialize(GoodQuestionsAsked);
            GoodQuestionsMissed = new List<Question>();
            foreach (Question _q in Dialogue)
            {
                if(_q.Relevance == 1)
                {
                    if (!GoodQuestionsAsked.Contains(_q))
                    {
                        GoodQuestionsMissed.Add(_q);
                    }
                }
            }
            GoodQuestionsMissedHandler.Initialize(GoodQuestionsMissed);
        }

        public void SeenQuestion(Question _q)
        {
            _q.isNew = false;
            Refresh();
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
            PatientSource.clip = Resources.Load<AudioClip>("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + DialogueName + "/waiting_for_too_long") as AudioClip;
            PatientSource.Play();
        }

        IEnumerator AnswerRepeated(AudioClip _ac)
        {
            PatientSource.clip = Resources.Load<AudioClip>("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + DialogueName + "/repeated_question") as AudioClip;
            PatientSource.Play();
            while (PatientSource.isPlaying)
            {
                yield return 0;
            }
            PatientSource.clip = _ac;
            PatientSource.Play();
        }

        #if UNITY_EDITOR

        public void SyncDialogues()
        {
            Dialogue.Clear();
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
