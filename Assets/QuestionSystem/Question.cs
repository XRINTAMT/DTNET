using UnityEngine;
using System.Collections.Generic;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {
        public Question(string[] values)
        {
            Text = new Dictionary<string, string>();
            Answer = new Dictionary<string, string>();
            Tag = values[0];
            Text["English"] = values[1];
            Text["German"] = values[2];
            Text["Swedish"] = values[3];
            Text["Lithuanian"] = values[4];
            Text["Latvian"] = values[5];
            MoodChanges = int.Parse(values[6]);
            AnimationType = values[7];
            Prerequisite = null;
            IsAsked = false;
            Answer["English"] = values[8];
            Answer["German"] = values[9];
            Answer["Swedish"] = values[10];
            Answer["Lithuanian"] = values[11];
            Answer["Latvian"] = values[12];
            PrerequisiteTag = values[13];
            Topic = values[14];
        }
        [field: SerializeField] public string Tag { get; set; }
        [field: SerializeField] public string Topic { get; set; }
        [field: SerializeField] public Dictionary<string, string> Text { get; set; }
        public int MoodChanges { get; set; }
        public string AnimationType { get; set; }
        [field: SerializeReference] public Question Prerequisite { get; set; }
        public bool IsAsked { get; set; }
        [field: SerializeField] public Dictionary<string, string> Answer { get; set; }
        public string PrerequisiteTag { get; set; }
        public bool PrerequisiteMet()
        {
            return Prerequisite == null || Prerequisite.IsAsked;
        }
    }

}