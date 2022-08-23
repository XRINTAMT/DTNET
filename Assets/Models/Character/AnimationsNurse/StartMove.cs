using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    [SerializeField] private Animation animationDoctorNurse;
    [SerializeField] private Animator nurseAnimator;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
