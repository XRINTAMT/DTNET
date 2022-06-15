using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DTNET.Handlers {
    public class AllTaskCompletedUIHandler : MonoBehaviour
    {
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
