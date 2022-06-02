using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DTNET.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void SetBeginnerInfoText() {
            TextMeshProUGUI infoTMP = GetGameModeInfoTextTMP();
            infoTMP.text = "You get hints that guides you through the tasks.";

        }

        public void SetExperienceInfoText() {
            TextMeshProUGUI infoTMP = GetGameModeInfoTextTMP();
            infoTMP.text = "No hints.";
        }


        private TextMeshProUGUI GetGameModeInfoTextTMP() {
            return this.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}
