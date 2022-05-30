using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace DTNET.UI 
{
    public class AllTasksCompleted : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }

        public void Show(string results) 
        {
            gameObject.SetActive(true);
            TextMeshProUGUI resultTMP = GetResultTextTMP();
            resultTMP.text = results;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private TextMeshProUGUI GetResultTextTMP() {
            return this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}
