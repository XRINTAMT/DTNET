using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestionSystem;
using UnityEngine.UI;

public class QuestionReport : MonoBehaviour
{
    ReportListHandler listHandler;
    Question question;
    [SerializeField] Text QuestionShortText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Question _q = null, ReportListHandler _handler = null)
    {
        if (_q == null)
        {
            gameObject.SetActive(false);
            return;
        }
        listHandler = _handler;
        question = _q;
        string _lang = PlayerPrefs.GetString("Language", "English");
        QuestionShortText.text = _q.Short[_lang];
    }

    public void Submit()
    {
        if (question.Asked["English"] != "")
        {
            listHandler.OpenPopup(question);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
