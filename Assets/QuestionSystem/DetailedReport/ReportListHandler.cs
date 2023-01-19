using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestionSystem;

public class ReportListHandler : MonoBehaviour
{
    [SerializeField] GameObject RecordsContainer;
    [SerializeField] CommentaryPopup Popup;
    [SerializeField] bool asked;
    [SerializeField] QuestionReport[] _reports;

    void Start()
    {
        
    }

    public void Initialize(List<Question> _questions)
    {
        RectTransform _containerTransform = RecordsContainer.GetComponent<RectTransform>();
        _containerTransform.sizeDelta = new Vector2(_containerTransform.sizeDelta.x, _questions.Count * 68 + 4);
        _reports = GetComponentsInChildren<QuestionReport>();
        int i = 0;
        for(; i < _questions.Count; i++)
        {
            _reports[i].Initialize(_questions[i],this);
        }
        for (; i < _reports.Length; i++)
        {
            _reports[i].Initialize();
        }
    }

    public void OpenPopup(Question _q)
    {
        string _lang = PlayerPrefs.GetString("Language", "English");
        if (_q != null)
        {
            Popup.Open(_q.Text[_lang], (asked ? _q.Asked[_lang] : _q.Missed[_lang]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
