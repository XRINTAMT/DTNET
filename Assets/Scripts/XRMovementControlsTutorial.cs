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
        Teleporter[] array = FindObjectsOfType<Teleporter>(true);
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].transform.parent.GetComponent<Hand>() && array[i].transform.parent.GetComponent<Hand>().left)
            {
                array[i].GetComponent<XRTeleporterLink>().role = UnityEngine.XR.XRNode.LeftHand;
            }
         
            if (array[i].transform.parent.GetComponent<Hand>() && !array[i].transform.parent.GetComponent<Hand>().left)
            {
                array[i].GetComponent<XRTeleporterLink>().role = UnityEngine.XR.XRNode.RightHand;
            }

            foreach (SpriteRenderer sr in array[i].gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            {
                sr.gameObject.SetActive(true);
            }
        }
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
        Teleporter[] array = FindObjectsOfType<Teleporter>(true);
        for (int i = 0; i < array.Length; i++)
        {
            array[i].enabled = true;
            array[i].gameObject.SetActive(true);
            array[i].GetComponent<XRTeleporterLink>().enabled=true;
        }
        Teleport.SetActive(true);
    }
    public void SwitchLocomotionHand()
    {
        autoHandPlayer.teleporterL = teleporterHand;
        autoHandPlayer.xrTeleporterLinkLeft = xRTeleporterLinkHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
