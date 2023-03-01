using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    public class TopicTabsManager : MonoBehaviour
    {
        [SerializeField] TabButtonBehaviour[] TabButtons;
        List<string> topics;
        int currentPage = 0;
        int totalPages = 1;
        Coroutine rotation;

        public void Refresh(List<string> _unlockedTopics, List<string> _topicsWithNew)
        {
            topics = _unlockedTopics;
            int i = 0;
            foreach (string topic in _unlockedTopics)
            {
                TabButtons[i].Refresh(topic,_topicsWithNew.Contains(topic));
                if(i<8)
                    i++;
            }
            for (; i < TabButtons.Length; i++)
            {
                TabButtons[i].Refresh(null);
            }
            totalPages = (int)Mathf.Ceil((float)_unlockedTopics.Count / 3);
            if(totalPages == 2)
            {
                for (i = 0; i < 3; i++)
                {
                    TabButtons[i + 6].Refresh(topics[i], _topicsWithNew.Contains(topics[i]));
                }
            }
            while(topics.Count < 9)
            {
                topics.Add("null");
            }
        }

        public void RefreshTopic(string _topic)
        {
            foreach(TabButtonBehaviour _tab in TabButtons)
            {
                _tab.Activate(_tab.buttonTopic == _topic);
            }
        }

        public void Rotate()
        {
            if (rotation != null)
            {
                return;
            }
            if (totalPages == 1)
            {
                return;
            }
            if (currentPage < 2)
            {
                currentPage++;
                rotation = StartCoroutine(IERotationAnimation(120 * currentPage)); 
                return;
            }
            if (currentPage == 2)
            {                
                if(totalPages == 2)
                {
                    /*
                    Debug.Log("switching");
                    //switch
                    string[] temp1 = new string[3];
                    string[] temp2 = new string[3];
                    for(int i = 0; i < 3; i++)
                    {
                        temp1[i] = topics[i];
                        temp2[i] = topics[i+3];
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        topics[i] = temp2[i];
                        topics[i + 3] = temp1[i];
                        topics[i + 6] = temp2[i];
                    }

                    for (int i = 0; i < 9; i++)
                    {
                        TabButtons[i].Refresh(topics[i]);
                    }
                    */
                    currentPage = 1;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    currentPage = 0;
                }
                rotation = StartCoroutine(IERotationAnimation(120 * currentPage));
            }
        }

        IEnumerator IERotationAnimation(float endAngle)
        {
            float startAngle = transform.localRotation.eulerAngles.y;   
            for(float i = 0; i < 1; i += Time.deltaTime*2)
            {
                transform.localRotation = Quaternion.Euler(0,Mathf.LerpAngle(startAngle, endAngle, i),0);
                yield return 0;
            }
            transform.localRotation = Quaternion.Euler(0, endAngle, 0);
            rotation = null;
        }

        void Start()
        {
            topics = new List<string>(9);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
