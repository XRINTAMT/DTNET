using UnityEngine;
using System.Collections.Generic;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {
        public Question(string[] values)
        {
            valuesText = new string[10];
            Tag = values[0];
            valuesText[0] = values[1];
            valuesText[1] = values[2];
            
            MoodChanges = int.Parse(values[3]);
            AnimationType = values[4];
            Prerequisite = null;
            IsAsked = 0;
            PrerequisiteTag = values[5];
            Topic = values[6];
            InformationTag = values[7];

            valuesText[2] = values[8];
            valuesText[3] = values[9];
            valuesText[4] = values[10];
            valuesText[5] = values[11];
            valuesText[6] = values[12];
            valuesText[7] = values[13];
            valuesText[8] = values[14];
            valuesText[9] = values[15];
            
        }
        [field: SerializeField] public string Tag { get; set; }
        [field: SerializeField] public string Topic { get; set; }
        [field: SerializeField] public Dictionary<string, string> Text { get; set; }
        public int MoodChanges { get; set; }
        public string AnimationType { get; set; }
        [field: SerializeReference] public Question Prerequisite { get; set; }
        [field: SerializeField] public int IsAsked { get; set; }
        [field: SerializeField] public Dictionary<string, string> Answer { get; set; }
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
            //populate dictionaries from arrays because Unity can't serialize dictionaries
            Text["English"] = valuesText[0];
            Answer["English"] = valuesText[1];
            Text["German"] = valuesText[2];
            Answer["German"] = valuesText[3];
            Text["Swedish"] = valuesText[4];
            Answer["Swedish"] = valuesText[5];
            Text["Lithuanian"] = valuesText[6];
            Answer["Lithuanian"] = valuesText[7];
            Text["Latvian"] = valuesText[8];
            Answer["Latvian"] = valuesText[9];
        }
    }

}