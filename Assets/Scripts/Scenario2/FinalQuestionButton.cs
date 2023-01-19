using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Assets.SimpleLocalization;

public class FinalQuestionButton : MonoBehaviour
{
    [SerializeField] Image BackgroundImage;
    int isCorrect; //0 - incorrect, 1 - ok, 2 - excellent
    string patient;
    [SerializeField] UnityEvent OnShownAnswer;
    [SerializeField] Color[] ScoreColors;
    [SerializeField] LocalizedText Question;
    [SerializeField] LocalizedText Result;
    Color initColor;
    // Start is called before the first frame update
    void Start()
    {
        initColor = BackgroundImage.color;
    }

    public void Setup(string _patient, int _score)
    {
        isCorrect = _score;
        patient = _patient;
        Question.LocalizationKey = patient + "_" + isCorrect;
        Question.Localize();
    }

    public void Submit()
    {
        BackgroundImage.color = ScoreColors[isCorrect];
        Result.LocalizationKey = "QuizAnswer_" + isCorrect;
        Result.Localize();
        OnShownAnswer.Invoke();
    }

    public void Deselect()
    {
        BackgroundImage.color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
