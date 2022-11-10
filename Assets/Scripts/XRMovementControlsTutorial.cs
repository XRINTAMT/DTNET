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
        AutoHandPlayer.teleportMove = true;
        Teleport.SetActive(true);
    }
    public void SwitchLocomotionMove()
    {
        AutoHandPlayer.teleportMove = false;
        Teleport.SetActive(false);
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
