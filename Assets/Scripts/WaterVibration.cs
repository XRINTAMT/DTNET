using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class WaterVibration : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hand>())
        {
            other.GetComponent<Hand>().PlayHapticVibration(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
