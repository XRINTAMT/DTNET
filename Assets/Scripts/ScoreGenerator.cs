using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;
using UnityEngine.UI;

public class ScoreGenerator : MonoBehaviour
{
    [SerializeField] RatingRecord[] Records;
    [SerializeField] Text Total;
    [SerializeField] GameObject TextPerfect;
    [SerializeField] GameObject TextAverage;
    [SerializeField] GameObject TextPoor;

    public void Refresh(List<TaskSettings> tasks)
    {
        int i = 0;
        int totalScore = 0;
        int totalMax = 0;
        foreach (TaskSettings currentTask in tasks)
        {
            Debug.Log("checking " + currentTask.maxScore);
            if (currentTask.maxScore == 0)
            {
                continue;
            }
            else
            {
                Records[i].Refresh(currentTask.Score, currentTask.maxScore);
                totalScore += currentTask.Score;
                totalMax += currentTask.maxScore;
                i += 1;
            }
        }

        //trash "task" evaluation
        int _trash_task = 1;
        Trash[] TrashPieces = FindObjectsOfType<Trash>();
        foreach(Trash _trash in TrashPieces)
        {
            if (_trash.gameObject.activeInHierarchy)
            {
                _trash_task = 0;
                break;
            }
        }
        Records[7].Refresh(_trash_task, 1);
        totalScore += _trash_task;
        totalMax += 1;

        Pump_ConnectTubing _pump = FindObjectOfType<Pump_ConnectTubing>();

        int _expired_task = (_pump.Expired) ? 0 : 1;
        Records[8].Refresh(_expired_task, 1);
        totalScore += _expired_task;
        totalMax += 1;

        //for normal ranking
        //Total.text = totalScore.ToString() + "/" + totalMax.ToString();
        TextPerfect.SetActive(totalScore == totalMax);
        TextAverage.SetActive((totalScore != totalMax) && (totalScore > totalMax / 2));
        TextPoor.SetActive(totalScore <= totalMax / 2);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
