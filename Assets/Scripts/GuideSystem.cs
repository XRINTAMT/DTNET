using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    [SerializeField] private List <GameObject> guidePanel;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject dialogueGuide;
    [SerializeField] GameObject arrowObservationSheet;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("GuidedMode") == 1)
        {
            if (dialogueGuide != null) dialogueGuide.SetActive(true);
            if (arrowObservationSheet != null) arrowObservationSheet.SetActive(true);
        }
    }

    public void GuidePanelActivate(int numberTask) 
    {
        bool disactivateAll = false;

        for (int i = 0; i < guidePanel.Count; i++)
        {
            guidePanel[i].SetActive(false);
           
        }
        disactivateAll = true;

        if (disactivateAll)
        {
            guidePanel[numberTask].SetActive(true);
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
