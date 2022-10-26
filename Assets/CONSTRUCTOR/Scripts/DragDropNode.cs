using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropNode : MonoBehaviour
{
    [SerializeField] float OffsetX;
    [SerializeField] float OffsetY;
    [SerializeField] float OffsetZ;

    bool startFollow;

    private void Start()
    {

    }


    public void PointDown() 
    {
        startFollow = true;
        DialogueEditor.moveObj = true;

    }
    public void PointUp()
    {
        startFollow = false;
        DialogueEditor.moveObj = false;
    }
  

    void Update()
    {
        if (startFollow)
        {
            Debug.Log(33);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + OffsetZ));
            transform.position = mousePosition;
        }

    }
 
}
