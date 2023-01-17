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
        [SerializeField] List<string> totalInformation;
        [SerializeField] List<string> unlockedInformation;
        [SerializeField] GameObject PatientObject;
        [SerializeField] TopicTabsManager Tabs;
        [SerializeField] ChoiceMenu CMenu;
        [SerializeField] DialogueLines DLines;
        [SerializeField] CompletedScenario OutroScreen;
        [SerializeField] Text NameText;
        [SerializeField] Text DateOfBirthText;
        [SerializeField] int mood = 0;
        [SerializeField] int questionsLimit = 20;
        [SerializeField] AudioSource NurseSource;
        [SerializeField] string Name;
        [SerializeField] string DateOfBirth;
        [SerializeField] List<Sprite> moodList;
        [SerializeField] int HappyThreshold = 0;
        [SerializeField] int SadThreshold = -5;
        [SerializeField] Image MoodIndicator;


        string NurseGender = "Female";        
        AudioSource PatientSource;
        FaceAnimationController FAController;
        Animator BodyAnimator;

        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        [SerializeField] float TimeForAsking = 20;
        Coroutine QuestionTimeout;
        public bool Sync;
        int questionsCount = 0;


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
            foreach (Question question in Dialogue)
            {
                if (!question.PrerequisiteMet())
                    continue;
                if (!unlockedTopics.Contains(question.Topic))
                    unlockedTopics.Add(question.Topic);
            }
            Tabs.Refresh(unlockedTopics);
        }

        public void ChangeTopic(string _topic, bool _newTopic = true)
        {
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
            if(QuestionTimeout != null)
                StopCoroutine(QuestionTimeout);
            CMenu.gameObject.SetActive(false);
            DLines.gameObject.SetActive(true);
            DLines.RenderQuestion(_q);
            NurseSource.clip = Resources.Load<AudioClip>("DialogueAudios/"+PlayerPrefs.GetString("Language","English")+"/"+NurseGender+ "Nurse/" + _q.Tag) as AudioClip;
            NurseSource.Play();
            Refresh();
        }

        public void ProcessAnswer(Question _q)
        {
            if (_q != null)
            {
                if(_q.Tag == "end_scenario")
                {
                    OutroScreen.SetData(MoodSprite(mood),totalInformation.Count, unlockedInformation.Count, DialogueName,"","");
                    gameObject.SetActive(false);
                    //shove in a normal mood-based thing to output the mood on the ending screen
                }
                if(_q.Tag == "introduction")
                {
                    NameText.text = Name;
                }
                if (_q.Tag == "date_of_birth")
                {
                    DateOfBirthText.text = DateOfBirth;
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

                MoodIndicator.sprite = MoodSprite(mood);
                FAController.SetMoodIndex(100 + (mood * 2));
                BodyAnimator.SetTrigger(_q.AnimationType);
                //FAController.SetMood(mood);
                //need a method SetMood(mood) in FaceAnimationController that would
                //set the mood to this value (optionally with an overshoot)

                //load the audio for the patient here
                PatientSource.clip = Resources.Load<AudioClip>("DialogueAudios/" + PlayerPrefs.GetString("Language", "English") + "/" + DialogueName + "/" + _q.Tag) as AudioClip;
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
                OutroScreen.SetData(MoodSprite(mood), totalInformation.Count, unlockedInformation.Count, DialogueName,"","");
                gameObject.SetActive(false);
            }
            ChangeTopic(_q.Topic, false);
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
