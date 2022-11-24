using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaleMove : MonoBehaviour
{
    [SerializeField] private Transform FollowLeft;
    [SerializeField] private Transform FollowRight;

    [SerializeField] private Transform HandLeft;
    [SerializeField] private Transform HandRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Pause() 
    {
        Time.timeScale = 0;
    }
    public void Play()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            HandLeft.transform.position = FollowLeft.transform.position;
            HandLeft.transform.rotation = FollowLeft.transform.rotation;

            HandRight.transform.position = FollowRight.transform.position;
            HandRight.transform.rotation = FollowRight.transform.rotation;
        }
    }
}
