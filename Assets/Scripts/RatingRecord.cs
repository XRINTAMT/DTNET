using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingRecord : MonoBehaviour
{
    [SerializeField] Text scoreText;

    void Start()
    {
        
    }

    public void Refresh(int score, int maxScore)
    {
        //for normal scores
        //scoreText.text = score.ToString() + "/" + maxScore.ToString();
        scoreText.text = (score == maxScore) ? "V" : "X";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
