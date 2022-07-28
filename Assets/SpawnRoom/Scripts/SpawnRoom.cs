using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteAlways]

public class SpawnRoom : MonoBehaviour
{ 
    [SerializeField] private GameObject roomPref; 
    [Header("")]
    [SerializeField] private float widh;
    [SerializeField] private float lenght;
    [SerializeField] private float height;
    GameObject instRoom;
   
    public void Start()
    {
        if (transform.childCount >= 6) DestroyImmediate(transform.GetChild(5).gameObject);
        
        Spawn();
    }
    public void Spawn()
    {
        if (transform.childCount >= 6) DestroyImmediate(transform.GetChild(5).gameObject);

        instRoom = Instantiate(roomPref, transform.position, transform.rotation,transform);
        instRoom.transform.localScale = new Vector3(widh, 1, lenght);

        InstRoom();

    }
    void InstRoom()
    {
        Material mat;
        instRoom.transform.GetChild(0).transform.localScale = new Vector3(instRoom.transform.GetChild(0).localScale.x, 0.1f, instRoom.transform.GetChild(0).localScale.z);
        instRoom.transform.GetChild(1).transform.localScale = new Vector3(height, 0.01f, instRoom.transform.GetChild(1).localScale.z);
        instRoom.transform.GetChild(2).transform.localScale = new Vector3(height, 0.01f, instRoom.transform.GetChild(2).localScale.z);
        instRoom.transform.GetChild(3).transform.localScale = new Vector3(height, 0.01f, instRoom.transform.GetChild(3).localScale.z);
        instRoom.transform.GetChild(4).transform.localScale = new Vector3(height, 0.01f, instRoom.transform.GetChild(4).localScale.z);
        instRoom.transform.GetChild(5).transform.localScale = new Vector3(instRoom.transform.GetChild(5).localScale.x, 0.1f, instRoom.transform.GetChild(5).localScale.z);
        instRoom.transform.GetChild(5).transform.position = new Vector3(instRoom.transform.GetChild(5).transform.position.x, height, instRoom.transform.GetChild(5).transform.position.z);
        mat = instRoom.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        mat.mainTextureScale = new Vector3(1 * (widh), 1 * (lenght));
    }
}
#endif