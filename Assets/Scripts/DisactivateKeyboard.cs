using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisactivateKeyboard : MonoBehaviour
{
    [SerializeField] SheetController sheetController;
    // Start is called before the first frame update
   
    void Start()
    {
        sheetController = GameObject.Find("SheetPrafabObservationSheet").GetComponent<SheetController>();
    }

    public void Disactivate() 
    {
        sheetController.keyboard.SetActive(false);   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
