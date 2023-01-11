using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinalQuestionButton : MonoBehaviour
{
    [SerializeField] Image BackgroundImage;
    public int IsCorrect; //0 - incorrect, 1 - ok, 2 - excellent
    [SerializeField] UnityEvent OnShownAnswer;
    [SerializeField] Color[] ScoreColors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Submit()
    {
        BackgroundImage.color = ScoreColors[IsCorrect];
        OnShownAnswer.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
