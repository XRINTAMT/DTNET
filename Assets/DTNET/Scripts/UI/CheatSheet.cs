using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.UI 
{
    public class CheatSheet : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string gameMode = CSystem.GameMode.SelectedMode;
            if(gameMode == "Experience") {
                gameObject.SetActive(false);
            }
            else {
                gameObject.SetActive(true);
            }
        }
    }
}
