using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    [SerializeField] private List <GameObject> guidePanel;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject dialogueGuide;
    [SerializeField] GameObject arrowObservationSheet;
    public Action<int> activateGuide;
    bool dontRepeat;
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
        activateGuide?.Invoke(numberTask);

        if (numberTask == 1 && !dontRepeat)
        {
            guidePanel[0].SetActive(false);
            guidePanel[1].SetActive(true);
            dontRepeat = true;
        }
        if (numberTask != 1)
        {
            for (int i = 0; i < guidePanel.Count; i++)
                guidePanel[i].SetActive(false);

            guidePanel[numberTask].SetActive(true);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
