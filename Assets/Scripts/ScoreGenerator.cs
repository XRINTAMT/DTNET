using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;
using UnityEngine.UI;

public class ScoreGenerator : MonoBehaviour
{
    [SerializeField] RatingRecord[] Records;
    [SerializeField] Text Total;

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
        Total.text = totalScore.ToString() + "/" + totalMax.ToString();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
