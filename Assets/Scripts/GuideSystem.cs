using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSystem : DataSaver
{
    [SerializeField] private List <GameObject> guidePanel;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject dialogueGuide;
    [SerializeField] GameObject arrowObservationSheet;
    public Action<int> activateGuide;
    bool dontRepeat;
    bool savedDontRepeat;
    int lastPanelActivated;
    int lastActivatedPanelSaved;

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
        lastPanelActivated = numberTask;
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

    public override void Save()
    {
        //lastActivatedPanelSaved = lastPanelActivated;
        //savedDontRepeat = dontRepeat;
    }

    public override void Load()
    {
        //dontRepeat = savedDontRepeat;
        //GuidePanelActivate(lastActivatedPanelSaved);
    }
}
