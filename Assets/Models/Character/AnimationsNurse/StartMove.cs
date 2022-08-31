using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    [SerializeField] private Animation animationDoctorNurse;
    [SerializeField] private Animator nurseAnimator;
    [SerializeField] private AnimationsController animationsController;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void move()
    {
        animationDoctorNurse.Play();
    }
    public void applyRootMotion()
    {
        //nurseAnimator.applyRootMotion=true;
    }
    public void makeInject() 
    {
        animationsController.AnimationStopNurse();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
