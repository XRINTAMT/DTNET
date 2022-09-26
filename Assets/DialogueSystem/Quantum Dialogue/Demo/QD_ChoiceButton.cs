using UnityEngine;
using UnityEngine.UI;

namespace QuantumTek.QuantumDialogue.Demo
{
    public class QD_ChoiceButton : MonoBehaviour
    {
        public int number;
        public QD_DialogueDemo demo;
        public string text;
        public GameObject dialogue;
        private void Start()
        {
            text = GetComponent<Text>().text;
        }

        public void SelectButton() 
        {
            if (text!= "Close")
            {
                demo.Choose(number);
            }
            if (text == "Close")
            {
                Debug.Log("Close");
                dialogue.SetActive(false);
            }
            if (text == "Give the observation data to doctor")
            {
                dialogue.SetActive(false);
            }

        }
        public void Select() => demo.Choose(number);
    }
}