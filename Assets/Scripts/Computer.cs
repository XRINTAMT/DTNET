using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace dtnet
{
    public class Computer : MonoBehaviour
    {

        public GameObject computerCanvas;
        public GameObject mainPanel;
        public GameObject referralPanel;
        public bool isDisplayScreen;
        public UnityEvent onPressed;

        // Start is called before the first frame update
        void Start()
        {
            HideScreen();
            DisplayMainPanel();
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter!");
            onPressed.Invoke();
        }

        private void DisplayScreen()
        {
            computerCanvas.SetActive(true);
        }

        private void HideScreen()
        {
            computerCanvas.SetActive(false);
        }


        public void DisplayMainPanel()
        {
            mainPanel.SetActive(true);
            referralPanel.SetActive(false);
        }

        public void DisplayReferralPanel()
        {
            mainPanel.SetActive(false);
            referralPanel.SetActive(true);
        }


        public void PressedComputer()
        {
            Debug.Log("PressedComputer!!!");
            isDisplayScreen = !isDisplayScreen;
            if(isDisplayScreen)
            {
                DisplayScreen();
            }
            else
            {
                HideScreen();
            }
        }

    }
}
