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
        scoreText.text = score.ToString() + "/" + maxScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
