using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WashingHand : MonoBehaviour
{
    //public UnityEvent washingHand = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="hand")
        {
            GameObject hand = null;
            hand = other.gameObject;
            hand.GetComponent<Hand>().PlayHapticVibration(1);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
