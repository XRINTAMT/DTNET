using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConnectionControllerNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] float OffsetX;
    [SerializeField] public float OffsetY;
    [SerializeField] public float OffsetZ;

    Transform ParentNode;
    Transform ParentContent;
    Transform connectedPoint;  
    
    Vector3 startPos;

    bool connected;
    bool pointerEnter;
    bool startFollow;
    bool onTrigger;
    

    private void Start()
    {
        startPos = GetComponent<Transform>().localPosition;
        ParentNode = transform.parent.parent.parent;
        ParentContent = transform.parent;

        transform.parent = ParentContent;
        transform.localPosition = startPos;

        connected = false;
        pointerEnter = false;
        startFollow = false;
        onTrigger = false;

    }

    public void DeleteConnection() 
    {
        transform.parent = ParentContent;
        transform.localPosition = startPos;

        connected = false;
        pointerEnter = false;
        startFollow = false;
        onTrigger = false;
    }
    void ChangeIndexPos() 
    {
        if (ParentNode != null) ParentNode.SetSiblingIndex(ParentNode.parent.childCount - 1);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTrigger = true;
        connectedPoint = collision.gameObject.transform;
        if (connectedPoint.GetChild(0).GetComponent<Image>() != null)
        {
            connectedPoint.GetChild(0).GetComponent<Image>().color = Color.green;
            ChangeIndexPos();
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTrigger = false;
        if (connectedPoint.GetChild(0).GetComponent<Image>() !=null)
        {
            connectedPoint.GetChild(0).GetComponent<Image>().color = Color.grey;
        }
      
    }
    void Update()
    {
       

        if (Input.GetMouseButton(0) && pointerEnter && !DialogueEditor.moveObj)
        {
            startFollow = true;
         
            DialogueEditor.moveObj = true;

        }
        if (Input.GetMouseButtonUp(0) && pointerEnter)
        {
            startFollow = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            DialogueEditor.moveObj = false;

            if (onTrigger)
            {
                connected = true;
                transform.parent = connectedPoint;
                transform.position = connectedPoint.position;

            }
            if (!onTrigger && connected)
            {
                connected = false;
                transform.parent = ParentContent;
            }
            if (!connected)
            {
                transform.localPosition = startPos;
            }
            
        }

        if (startFollow && !connected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + OffsetZ));
            transform.position = mousePosition;
        }

       
    }
   
}

