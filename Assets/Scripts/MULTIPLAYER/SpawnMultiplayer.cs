using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnMultiplayer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Multipalyer",Vector3.zero,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
