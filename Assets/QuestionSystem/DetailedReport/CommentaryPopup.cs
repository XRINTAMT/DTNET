using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentaryPopup : MonoBehaviour
{
    [SerializeField] Text Question;
    [SerializeField] Text Commentary;
    [SerializeField] GameObject Popup;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Open(string _question, string _commentary)
    {
        Popup.SetActive(true);
        Question.text = _question;
        Commentary.text = _commentary;
    }

    public void Close()
    {
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
