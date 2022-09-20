using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;

public class XRMovementControls : MonoBehaviour
{
    [SerializeField] XRHandPlayerControllerLink MovementControls;
    [SerializeField] GameObject Teleport;
    [SerializeField] XRHandControllerLink RealHand;
    [SerializeField] XRHandControllerLink DummyHand;
    void Awake()
    {
        int MovementType = PlayerPrefs.GetInt("MovementType", 0);
        Debug.Log("Loaded movement type as " + MovementType);
        SwitchLocomotion(MovementType);
    }

    public void SwitchLocomotion(int type)
    {
        if (type == 0)
        {
            Teleport.SetActive(true);
            MovementControls.moveController = DummyHand;
        }
        else
        {
            Teleport.SetActive(false);
            MovementControls.moveController = RealHand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
