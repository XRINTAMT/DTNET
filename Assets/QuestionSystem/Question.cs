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
            valuesText[2] = values[3];
            valuesText[3] = values[4];
            valuesText[4] = values[5];
            MoodChanges = int.Parse(values[6]);
            AnimationType = values[7];
            Prerequisite = null;
            IsAsked = false;
            valuesText[5] = values[8];
            valuesText[6] = values[9];
            valuesText[7] = values[10];
            valuesText[8] = values[11];
            valuesText[9] = values[12];
            PrerequisiteTag = values[13];
            Topic = values[14];
        }
        [field: SerializeField] public string Tag { get; set; }
        [field: SerializeField] public string Topic { get; set; }
        [field: SerializeField] public Dictionary<string, string> Text { get; set; }
        public int MoodChanges { get; set; }
        public string AnimationType { get; set; }
        [field: SerializeReference] public Question Prerequisite { get; set; }
        [field: SerializeField] public bool IsAsked { get; set; }
        [field: SerializeField] public Dictionary<string, string> Answer { get; set; }
        [field: SerializeField] public string PrerequisiteTag { get; set; }
        public bool PrerequisiteMet()
        {
            return Prerequisite == null || Prerequisite.IsAsked;
        }

        [field: SerializeField] private string[] valuesText;

        public void GetReady()
        {
            Text = new Dictionary<string, string>();
            Answer = new Dictionary<string, string>();
            //populate dictionaries from arrays because Unity can't serialize dictionaries
            Text["English"] = valuesText[0];
            Text["German"] = valuesText[1];
            Text["Swedish"] = valuesText[2];
            Text["Lithuanian"] = valuesText[3];
            Text["Latvian"] = valuesText[4];
            Answer["English"] = valuesText[5];
            Answer["German"] = valuesText[6];
            Answer["Swedish"] = valuesText[7];
            Answer["Lithuanian"] = valuesText[8];
            Answer["Latvian"] = valuesText[9];
        }
    }

}