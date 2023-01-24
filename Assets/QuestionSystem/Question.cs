using UnityEngine;
using System.Collections.Generic;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {

        static string[] Languages = { "English", "German", "Swedish", "Lithuanian", "Latvian" };
        public Question(string[] values)
        {
            valuesText = new string[25];
            Tag = values[0];
            valuesText[0] = values[1];
            valuesText[1] = values[2];
            valuesText[2] = values[3];
            valuesText[3] = values[4];
            valuesText[4] = values[5];

            MoodChanges = int.Parse(values[6]);
            AnimationType = values[7];
            Prerequisite = null;
            IsAsked = 0;
            PrerequisiteTag = values[8];
            Topic = values[9];
            InformationTag = values[10];
            Relevance = int.Parse(values[11]);

            for(int i = 5; i < valuesText.Length; i++)
            {
                valuesText[i] = values[i + 7];
            }
        }
        [field: SerializeField] public string Tag { get; set; }
        [field: SerializeField] public string Topic { get; set; }
        [field: SerializeField] public Dictionary<string, string> Text { get; set; }
        [field: SerializeField] public int MoodChanges { get; set; }
        [field: SerializeField] public string AnimationType { get; set; }
        [field: SerializeReference] public Question Prerequisite { get; set; }
        [field: SerializeField] public int IsAsked { get; set; }
        [field: SerializeField] public int Relevance { get; set; }
        [field: SerializeField] public Dictionary<string, string> Answer { get; set; }
        [field: SerializeField] public Dictionary<string, string> Short { get; set; }
        [field: SerializeField] public Dictionary<string, string> Asked { get; set; }
        [field: SerializeField] public Dictionary<string, string> Missed { get; set; }
        [field: SerializeField] public string PrerequisiteTag { get; set; }
        [field: SerializeField] public string InformationTag { get; set; }
        public bool PrerequisiteMet()
        {
            return Prerequisite == null || (Prerequisite.IsAsked > 0);
        }

        [field: SerializeField] private string[] valuesText;

        public void GetReady()
        {
            Text = new Dictionary<string, string>();
            Answer = new Dictionary<string, string>();
            Short = new Dictionary<string, string>();
            Asked = new Dictionary<string, string>();
            Missed = new Dictionary<string, string>();

            for (int i = 0; i < valuesText.Length; i++)
            {
                if (valuesText[i].Length > 0)
                {
                    if (valuesText[i][0] == '"')
                    {
                        valuesText[i] = valuesText[i].Trim('"');
                    }
                }
            }
            //populate dictionaries from arrays because Unity can't serialize dictionaries
            for (int i = 0; i < 5; i++)
            {
                int n = i * 5;
                Text[Languages[i]] = valuesText[n];
                Short[Languages[i]] = valuesText[n + 1];
                Answer[Languages[i]] = valuesText[n + 2];
                Asked[Languages[i]] = valuesText[n + 3];
                Missed[Languages[i]] = valuesText[n + 4];
                if (Short[Languages[i]] == "" || Short[Languages[i]] == "#VALUE!")
                {
                    Short[Languages[i]] = Text[Languages[i]];
                }
            }
        }
    }

}