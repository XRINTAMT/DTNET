using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{


    [SerializeField] float OffsetMoveX;
    [SerializeField] float OffsetMoveY;
    [SerializeField] float OffsetMoveZ;

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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + +OffsetMoveX, Input.mousePosition.y + OffsetMoveY, Camera.main.transform.position.y + OffsetMoveZ));
            transform.position = mousePosition;
        }

    }
}
