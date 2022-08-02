using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{

    [SerializeField] private Animation animationSeatDown;
    [SerializeField] private Animator patient1;
    [SerializeField] private Animator patient2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AnimationSeatDownPatient() 
    {
        animationSeatDown.Play();
        patient1.SetTrigger("Seat");
        patient2.SetTrigger("Seat");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
