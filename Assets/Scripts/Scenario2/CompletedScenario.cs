using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CompletedScenario : MonoBehaviour
{
    [SerializeField] GameObject panelCompletedScenario;
    [SerializeField] Image moodStatus;
    [SerializeField] List<Sprite> moodList;
    [SerializeField] Text answersProgress;
    [SerializeField] Text countMood;
    [SerializeField] Text informationUncovered;
    [SerializeField] Text nextStepsWithPatient;
    [SerializeField] FinalQuestionButton[] Questions;
    [SerializeField] GameObject Quiz;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject QuizResult;
    [SerializeField] Text ScenarioTime;

    string scenario;
    public bool activatePanel;
    PauseManager pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        pauseManager=GetComponent<PauseManager>();
    }
    public void SetData(Sprite mood, int totalInformation, int totlaFound, string scenarioName , string informationUncovered, string nextStepsWithPatient, double scenarioTime) 
    {
        pauseManager.ShowOutroMessage();
        panelCompletedScenario.SetActive(true);
        moodStatus.sprite = mood;
        
        answersProgress.text = "" + totlaFound  + "/" + totalInformation /*+ "%"*/;
        this.informationUncovered.text = "" + informationUncovered;
        this.nextStepsWithPatient.text = "" + nextStepsWithPatient;

        this.ScenarioTime.text = "Your time: " + ((int)(scenarioTime)) + " minutes";
        scenario = scenarioName;
        GiveQuestionsAway();

        string role = PlayerPrefs.GetString("Role", "Assistant");
        Quiz.SetActive(role == "Assistant");
        QuizResult.SetActive(role == "Assistant");
        Menu.SetActive(role != "Assistant");
    }

    private void GiveQuestionsAway()
    {
        int[] nums = { 0, 1, 2};
        System.Random rnd = new System.Random();
        int[] answers = nums.OrderBy(x => rnd.Next()).ToArray();
        for(int i = 0; i < answers.Length; i++)
        {
            Questions[i].Setup(scenario, answers[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
