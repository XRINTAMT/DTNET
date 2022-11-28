using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;
using Autohand;

public class XRMovementControlsTutorial : MonoBehaviour
{
    [SerializeField] XRHandPlayerControllerLink MovementControls;
    [SerializeField] GameObject Teleport;
    [SerializeField] Teleporter teleporterHand;
    [SerializeField] XRTeleporterLink xRTeleporterLinkHand;
    [SerializeField] AutoHandPlayer autoHandPlayer;
    void Start()
    {
      
    }

    public void SwitchLocomotionTeleport()
    {
        AutoHandPlayer.movementType = MovementType.Teleport;
        Teleport.SetActive(true);
    }
    public void SwitchLocomotionMove()
    {
        AutoHandPlayer.movementType = MovementType.Move;
        Teleport.SetActive(false);
    }
    public void SwitchLocomotionMixed()
    {
        AutoHandPlayer.movementType = MovementType.Mixed;
        Teleport.SetActive(true);
    }
    public void SwitchLocomotionHand()
    {
        autoHandPlayer.teleporterL = teleporterHand;
        autoHandPlayer.xrTeleporterLink = xRTeleporterLinkHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
