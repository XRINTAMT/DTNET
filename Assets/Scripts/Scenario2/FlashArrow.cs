using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartFlash() 
    {
        InvokeRepeating("FlashOn", 0, 1);
        InvokeRepeating("FlashOff", 0.5f, 1);
    }
    public void StopFlash()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }

    private void FlashOn() 
    {
        gameObject.SetActive(true);
    }
    private void FlashOff()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
