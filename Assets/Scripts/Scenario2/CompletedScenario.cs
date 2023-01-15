using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedScenario : MonoBehaviour
{
    [SerializeField] GameObject panelCompletedScenario;
    [SerializeField] Image moodStatus;
    [SerializeField] List<Sprite> moodList;
    [SerializeField] Text answersProgress;
    [SerializeField] Text countMood;
    [SerializeField] Text informationUncovered;
    [SerializeField] Text nextStepsWithPatient;

    public bool activatePanel;
    PauseManager pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        pauseManager=GetComponent<PauseManager>();
    }
    public void SetData(int indexMoodList, int totlaInformation, int totlaFound, string scenarioName , int countMood, string informationUncovered, string nextStepsWithPatient) 
    {
        pauseManager.ShowOutroMessage();
        panelCompletedScenario.SetActive(true);
        moodStatus.sprite = moodList[indexMoodList];
        answersProgress.text = "" + totlaFound  + "/" + totlaInformation /*+ "%"*/;
        this.countMood.text = "" + countMood;
        this.informationUncovered.text = "" + informationUncovered;
        this.nextStepsWithPatient.text = "" + nextStepsWithPatient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
