using UnityEngine;
using System.Collections;


public class DragDrop : MonoBehaviour
{

    private Vector3 offset;


    public float verticalOffset;
    public float horizontalOffset;

    public Transform panel;

    void Update()
    {
        var position = (Vector2)Input.mousePosition + (Vector2.up * verticalOffset) + (Vector2.left * horizontalOffset);
        panel.position = position;
    }
    void OnMouseDown()
    {
        Debug.Log(6);
        var position = (Vector2)Input.mousePosition + (Vector2.up * verticalOffset) + (Vector2.left * horizontalOffset);
        panel.position = position;

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