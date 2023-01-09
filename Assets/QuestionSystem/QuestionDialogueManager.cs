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
        [SerializeField] CompletedScenario OutroScreen;
        [SerializeField] int mood = 0;
        [SerializeField] int questionsLimit = 20;
        [SerializeField] AudioSource NurseSource;

        string NurseGender = "Female";        
        AudioSource PatientSource;
        FaceAnimationController FAController;
        Animator BodyAnimator;

        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        [SerializeField] float TimeForAsking = 20;
        Coroutine QuestionTimeout;
        public bool Sync;
        int questionsCount = 0;


        void Start()
        {
            PatientSource = PatientObject.GetComponent<AudioSource>();
            FAController = PatientObject.GetComponent<FaceAnimationController>();
            BodyAnimator = PatientObject.GetComponent<Animator>();
            totalInformation = new List<string>();
            unlockedInformation = new List<string>();
            for (int i = 0; i < Dialogue.Count;)
            {
                Dialogue[i].GetReady();
                if (Dialogue[i].Answer["English"] == "EMPTY")
                {
                    Dialogue.RemoveAt(i);
                }
                else
                {
                    if (!totalInformation.Contains(Dialogue[i].InformationTag))
                    {
                        totalInformation.Add(Dialogue[i].InformationTag);
                    }
                    i++;
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
            NurseGender = PlayerPrefs.GetInt("Gender") == 0 ? "Female" : "Male";
            if (NurseGender == "Male")
            {
                Debug.Log("Male detected");
                Dialogue[0].Text["English"] = Dialogue[0].Text["English"].Replace("Marite", "Alexander");
                Dialogue[0].Text["German"] = Dialogue[0].Text["German"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Swedish"] = Dialogue[0].Text["Swedish"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Latvian"] = Dialogue[0].Text["Latvian"].Replace("Marite", "Alexander");
                Dialogue[0].Text["Lithuanian"] = Dialogue[0].Text["Lithuanian"].Replace("Marite", "Alexander");
            }

            Refresh();
            ChangeTopic("Introduction");
            //QuestionTimeout = StartCoroutine(WaitingForTooLong());
            
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
            if(QuestionTimeout != null)
                StopCoroutine(QuestionTimeout);
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.RenderQuestion(_q);
            Debug.Log("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + NurseGender + "Nurse/" + _q.Tag + ".mp3");
            NurseSource.clip = Resources.Load<AudioClip>("DialogueAudios/"+PlayerPrefs.GetString("Language","English")+"/"+NurseGender+ "Nurse/" + _q.Tag) as AudioClip;
            NurseSource.Play();
            Refresh();
        }

        public void ProcessAnswer(Question _q)
        {
            if (_q != null)
            {
                if(_q.Topic == "Ending")
                {
                    OutroScreen.SetData((mood-mood),totalInformation.Count, unlockedInformation.Count); 
                    //shove in a normal mood-based thing to output the mood on the ending screen
                }
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
                BodyAnimator.SetTrigger(_q.AnimationType);
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
                OutroScreen.SetData((mood - mood), totalInformation.Count, unlockedInformation.Count);
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
