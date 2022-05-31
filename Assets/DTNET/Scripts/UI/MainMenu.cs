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
            infoTMP.text = "You can see a list of the necessary tasks you have to complete for finishing the scenario.";

        }

        public void SetExperienceInfoText() {
            TextMeshProUGUI infoTMP = GetGameModeInfoTextTMP();
            infoTMP.text = "You do NOT see a list of the tasks. You will have to remeber them.";
        }


        private TextMeshProUGUI GetGameModeInfoTextTMP() {
            return this.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}
