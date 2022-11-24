using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand.Demo;

public class ControlsAssigner : MonoBehaviour
{
    PlayerObject player;
    [SerializeField] XRControllerEvent[] LeftEvents, RightEvents;

    void Awake()
    {
        player = FindObjectOfType<PlayerObject>();
        for(int i = 0; i < LeftEvents.Length; i++)
        {
            LeftEvents[i].link = player.LeftHand;
        }
        for (int i = 0; i < RightEvents.Length; i++)
        {
            RightEvents[i].link = player.RightHand;
        }
    }

    void Update()
    {
        
    }
}
