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
        SwitchLocomotion(PlayerPrefs.GetInt("MovementType", 0));
    }

    public void SwitchLocomotion(int type)
    {
        switch (type)
        {

            case (0):
                AutoHandPlayer.movementType= MovementType.Teleport;
                Teleport.SetActive(true);
                break;
            case (1):
                AutoHandPlayer.movementType = MovementType.Move;
                Teleport.SetActive(false);
                break;
            case (2):
                AutoHandPlayer.movementType = MovementType.Mixed;
                Teleport.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
