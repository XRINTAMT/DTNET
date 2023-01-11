using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    public class TopicTabsManager : MonoBehaviour
    {
        [SerializeField] TabButtonBehaviour[] TabButtons;
        int currentPage = 0;
        int totalPages = 1;


        public void Refresh(List<string> _unlockedTopics)
        {
            int i = 0;
            foreach (string topic in _unlockedTopics)
            {
                TabButtons[i].Refresh(topic);
                if(i<8)
                    i++;
            }
            for (; i < TabButtons.Length; i++)
            {
                TabButtons[i].Refresh(null);
            }
            totalPages = (int)Mathf.Ceil((float)_unlockedTopics.Count / 3);
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
            if (totalPages == 1)
            {
                return;
            }
            if (currentPage < totalPages - 1)
            {
                currentPage++;
                StartCoroutine(IERotationAnimation(120 * currentPage)); 
                return;
            }
            if (currentPage == totalPages - 1)
            {
                currentPage = 0;
                StartCoroutine(IERotationAnimation(120 * currentPage));
            }
        }

        IEnumerator IERotationAnimation(float endAngle)
        {
            float startAngle = transform.rotation.eulerAngles.y;   
            for(float i = 0; i < 1; i += Time.deltaTime*2)
            {
                transform.rotation = Quaternion.Euler(0,Mathf.LerpAngle(startAngle, endAngle, i),0);
                yield return 0;
            }
            transform.rotation = Quaternion.Euler(0, endAngle, 0);
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
