using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddARoomPopup : MonoBehaviour
{
    [SerializeField] InputField roomNumber;
    [SerializeField] ViewerMenuBehaviour menuManager;
        
    public void Submit() 
    {
        //check if the room exists
        menuManager.AddRoom(roomNumber.text);
    }
}
