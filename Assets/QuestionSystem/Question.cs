using UnityEngine;
using System.Collections.Generic;

namespace QuestionSystem
{
    [System.Serializable]
    public class Question
    {
        public Question(string[] values)
        {
            valuesText = new string[15];
            Tag = values[0];
            valuesText[0] = values[1];
            valuesText[1] = values[2];
            valuesText[2] = values[3];

            MoodChanges = int.Parse(values[4]);
            AnimationType = values[5];
            Prerequisite = null;
            IsAsked = 0;
            PrerequisiteTag = values[6];
            Topic = values[7];
            InformationTag = values[8];

            valuesText[3] = values[9];
            valuesText[4] = values[10];
            valuesText[5] = values[11];
            valuesText[6] = values[12];
            valuesText[7] = values[13];
            valuesText[8] = values[14];
            valuesText[9] = values[15];
            valuesText[10] = values[16];
            valuesText[11] = values[17];
            valuesText[12] = values[18];
            valuesText[13] = values[19];
            valuesText[14] = values[20];

        }
        [field: SerializeField] public string Tag { get; set; }
        [field: SerializeField] public string Topic { get; set; }
        [field: SerializeField] public Dictionary<string, string> Text { get; set; }
        public int MoodChanges { get; set; }
        [field: SerializeField] public string AnimationType { get; set; }
        [field: SerializeReference] public Question Prerequisite { get; set; }
        [field: SerializeField] public int IsAsked { get; set; }
        [field: SerializeField] public Dictionary<string, string> Answer { get; set; }
        [field: SerializeField] public Dictionary<string, string> Short { get; set; }
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

            for(int i = 0; i < valuesText.Length; i++)
            {
                if(valuesText[i][0] == '"')
                {
                    valuesText[i] = valuesText[i].Trim('"');
                }
            }
            //populate dictionaries from arrays because Unity can't serialize dictionaries
            Text["English"] = valuesText[0];
            Short["English"] = valuesText[1];
            Answer["English"] = valuesText[2];

            Text["German"] = valuesText[3];
            Short["German"] = valuesText[4];
            Answer["German"] = valuesText[5];

            Text["Swedish"] = valuesText[6];
            Short["Swedish"] = valuesText[7];
            Answer["Swedish"] = valuesText[8];

            Text["Lithuanian"] = valuesText[9];
            Short["Lithuanian"] = valuesText[10];
            Answer["Lithuanian"] = valuesText[11];

            Text["Latvian"] = valuesText[12];
            Short["Latvian"] = valuesText[13];
            Answer["Latvian"] = valuesText[14];
        }
    }

}