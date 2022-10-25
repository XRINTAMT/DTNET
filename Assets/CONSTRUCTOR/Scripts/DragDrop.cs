using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;



    public bool pointerEnter;
    bool startFollow;

    private void Start()
    {
      
    }

    public void OnJoinFollow(GameObject obj) 
    {
        obj.transform.parent = transform;
    }
    public void OffJoinFollow(GameObject obj)
    {
        obj.transform.parent = transform.parent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEnter = false;
    }
    void Update()
    {
        //var position = (Vector2)Input.mousePosition + (Vector2.up * verticalOffset) + (Vector2.left * horizontalOffset);
        //transform.position = position;

        //pos = new Vector3(Input.mousePosition.x + verticalOffset, Input.mousePosition.y + horizontalOffset, zCameraDistance);
        //Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(pos);
        //transform.position = pos;

        if (Input.GetMouseButton(0) && pointerEnter && !DialogueEditor.moveObj)
        {
            startFollow = true;
            DialogueEditor.moveObj = true;
        }
        if (Input.GetMouseButtonUp(0) && pointerEnter)
        {
            startFollow = false;
            transform.position=new Vector3(transform.position.x, transform.position.y,0);
            DialogueEditor.moveObj = false;
        }

        if (startFollow)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + OffsetZ));
            transform.position = mousePosition;
        }

        //pos = new Vector3(Input.mousePosition.x + verticalOffset, Input.mousePosition.y+horizontalOffset, 1) ;
        //transform.position = pos;
    }
    //void OnMouseDown()
    //{
    //    Debug.Log(6);
    //    var position = (Vector2)Input.mousePosition + (Vector2.up * OffsetX) + (Vector2.left * OffsetY);
    //    transform.position = position;

    //    //offset = gameObject.transform.position -
    //    //    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    //}

    //void OnMouseDrag()
    //{
    //    Debug.Log(7);
    //    Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
    //    transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    //}
}