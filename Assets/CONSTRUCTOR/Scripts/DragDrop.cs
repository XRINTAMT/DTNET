using UnityEngine;
using System.Collections;


public class DragDrop : MonoBehaviour
{

    private Vector3 offset;


    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;

    Vector3 pos;
    float zCameraDistance;
    private void Start()
    {
        zCameraDistance=Camera.main.WorldToScreenPoint(transform.position).z;
    }
    void Update()
    {
        //var position = (Vector2)Input.mousePosition + (Vector2.up * verticalOffset) + (Vector2.left * horizontalOffset);
        //transform.position = position;

        //pos = new Vector3(Input.mousePosition.x + verticalOffset, Input.mousePosition.y + horizontalOffset, zCameraDistance);
        //Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(pos);
        //transform.position = pos;


        Vector3 mousePosition =Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + OffsetZ));
        transform.position = mousePosition;

        //pos = new Vector3(Input.mousePosition.x + verticalOffset, Input.mousePosition.y+horizontalOffset, 1) ;
        //transform.position = pos;
    }
    void OnMouseDown()
    {
        Debug.Log(6);
        var position = (Vector2)Input.mousePosition + (Vector2.up * OffsetX) + (Vector2.left * OffsetY);
        transform.position = position;

        //offset = gameObject.transform.position -
        //    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        Debug.Log(7);
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }
}