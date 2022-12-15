using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    [ExecuteInEditMode]
    public class QuestionLoader : MonoBehaviour
    {
        [SerializeField] string DialogueName;
        [field: SerializeField] public List<Question> Dialogue { get; private set; }
        public bool Sync;

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
