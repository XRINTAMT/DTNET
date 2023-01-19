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
        [SerializeField] GameObject guideDialogieUI;
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
                dialogue.SetActive(false);
                if (guideDialogieUI != null) guideDialogieUI.SetActive(true);               
            }
            if (text == "Give the observation data to doctor")
            {
                dialogue.SetActive(false);
            }

        }
        public void Select() => demo.Choose(number);
    }
}