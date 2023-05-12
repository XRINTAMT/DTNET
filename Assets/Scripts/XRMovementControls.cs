using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;
using Autohand;
using UnityEngine.SceneManagement;

public class XRMovementControls : MonoBehaviour
{
    [SerializeField] AutoHandPlayer AHPlayer;
    [SerializeField] GameObject TeleportRight;
    [SerializeField] GameObject TeleportLeft;
    AutoHandPlayer autoHandPlayer;
    //public int handType;
    //public int locomotionType;

    public MovementType movementType;
    public MovementHand handType;
    void Awake()
    {

        autoHandPlayer = FindObjectOfType<AutoHandPlayer>();
        SwitchLocomotion(PlayerPrefs.GetInt("MovementType", 0));
        AHPlayer.maxMoveSpeed = PlayerPrefs.GetFloat("walkingSpeed", 2);
    }

    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            TeleportRight.SetActive(false);
            TeleportLeft.SetActive(false);
            enabled = false;
            return;
        }


        TeleportRight.SetActive(true);
        TeleportLeft.SetActive(true);
        TeleportRight.GetComponent<XRTeleporterLink>().enabled = true;
        TeleportLeft.GetComponent<XRTeleporterLink>().enabled = true;

        SetHandType(handType, movementType);

    }
    public void SwitchLocomotion(int type)
    {
        switch (type)
        {

            case (0):
                AutoHandPlayer.movementType = MovementType.Teleport;
                //Teleport.SetActive(true);
                break;
            case (1):
                AutoHandPlayer.movementType = MovementType.Move;
                //Teleport.SetActive(false);
                break;
            case (2):
                AutoHandPlayer.movementType = MovementType.Mixed;
                //Teleport.SetActive(false);
                break;
        }
    }
    public void SwitchMovementHand(int type)
    {
        switch (type)
        {

            case (0):
                AutoHandPlayer.movementHand = MovementHand.Right;
                TeleportRight.SetActive(true);
                TeleportLeft.SetActive(false);
                break;
            case (1):
                AutoHandPlayer.movementHand = MovementHand.Left;
                TeleportRight.SetActive(false);
                TeleportLeft.SetActive(true);
                break;
           
        }

    }

    public void SetMovementSpeed(float speed)
    {
        AHPlayer.maxMoveSpeed = speed;
    }

    public void SetHandType(MovementHand handType, MovementType movementType)
    {
        TeleportRight.SetActive(true);
        TeleportLeft.SetActive(true);
        autoHandPlayer.handLeft.GetComponent<XRHandControllerLink>().enabled = true;
        autoHandPlayer.handRight.GetComponent<XRHandControllerLink>().enabled = true;


        if (handType == MovementHand.Left)
        {
            autoHandPlayer.xRHandPlayerControllerLink.moveController = autoHandPlayer.handLeft.GetComponent<XRHandControllerLink>();
            autoHandPlayer.xRHandPlayerControllerLink.turnController = autoHandPlayer.handRight.GetComponent<XRHandControllerLink>();
  
            foreach (Teleporter teleportRight in autoHandPlayer.handRight.GetComponentsInChildren<Teleporter>(true))
            {
                switch (movementType)
                {
                    case MovementType.Teleport:
                        teleportRight.enabled = false;
                        break;
                    case MovementType.Move:
                        teleportRight.enabled = false;
                        break;
                    case MovementType.Mixed:
                        teleportRight.enabled = true;
                        break;
                    default:
                        break;
                }
            }

            foreach (Teleporter teleportLeft in autoHandPlayer.handLeft.GetComponentsInChildren<Teleporter>(true))
            {
                switch (movementType)
                {
                    case MovementType.Teleport:
                        teleportLeft.enabled = true;
                        break;
                    case MovementType.Move:
                        teleportLeft.enabled = false;
                        break;
                    case MovementType.Mixed:
                        teleportLeft.enabled = true;
                        break;
                    default:
                        break;
                }
            }

        }

        if (handType==MovementHand.Right)
        {
            autoHandPlayer.xRHandPlayerControllerLink.moveController = autoHandPlayer.handRight.GetComponent<XRHandControllerLink>();
            autoHandPlayer.xRHandPlayerControllerLink.turnController = autoHandPlayer.handLeft.GetComponent<XRHandControllerLink>();

            foreach (Teleporter teleportRight in autoHandPlayer.handRight.GetComponentsInChildren<Teleporter>(true))
            {
                switch (movementType)
                {
                    case MovementType.Teleport:
                        teleportRight.enabled = true;
                        break;
                    case MovementType.Move:
                        teleportRight.enabled = false;
                        break;
                    case MovementType.Mixed:
                        teleportRight.enabled = true;
                        break;
                    default:
                        break;
                }
            }

            foreach (Teleporter teleportLeft in autoHandPlayer.handLeft.GetComponentsInChildren<Teleporter>(true))
            {
                switch (movementType)
                {
                    case MovementType.Teleport:
                        teleportLeft.enabled = false;
                        break;
                    case MovementType.Move:
                        teleportLeft.enabled = false;
                        break;
                    case MovementType.Mixed:
                        teleportLeft.enabled = true;
                        break;
                    default:
                        break;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
