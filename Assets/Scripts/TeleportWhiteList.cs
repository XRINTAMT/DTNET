using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWhiteList : MonoBehaviour
{
    [SerializeField] List <GameObject> teleportList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < teleportList.Count; i++)
        {
            teleportList[i].layer = 25;
        }
    }

}
