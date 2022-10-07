using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;
using Autohand;

public class XRMovementControls : MonoBehaviour
{
    [SerializeField] XRHandPlayerControllerLink MovementControls;
    [SerializeField] GameObject Teleport;

    void Awake()
    {
        //int MovementType = PlayerPrefs.GetInt("MovementType", 0);
        //Debug.Log("Loaded movement type as " + MovementType);
        //SwitchLocomotion(MovementType);
    }

    public void SwitchLocomotion(int type)
    {
        switch (type)
        {
            //case (-1):
            //    Teleport.SetActive(false);
            //    MovementControls.moveController = DummyHand;
            //    break;
            case (0):
                AutoHandPlayer.teleportMove = true;
                Teleport.SetActive(true);
                //MovementControls.moveController = DummyHand;
                break;
            case (1):
                AutoHandPlayer.teleportMove = false;
                Teleport.SetActive(false);
                //MovementControls.moveController = RealHand;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
