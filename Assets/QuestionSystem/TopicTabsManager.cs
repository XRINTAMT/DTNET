using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    public class TopicTabsManager : MonoBehaviour
    {
        [SerializeField] TabButtonBehaviour[] TabButtons;


        public void Refresh(List<string> unlockedTopics)
        {
            int i = 0;
            foreach (string topic in unlockedTopics)
            {
                TabButtons[i].Refresh(topic);
                i++;
            }
            for (; i < TabButtons.Length; i++)
            {
                TabButtons[i].Refresh(null);
            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
