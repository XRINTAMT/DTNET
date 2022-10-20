using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR

//[ExecuteAlways]

public class SpawnRoom : MonoBehaviour
{ 
    [SerializeField] private GameObject roomPref; 
    [Header("")]
    [SerializeField] private float width;
    [SerializeField] private float lenght;
    [SerializeField] private float height;

    [SerializeField] private Text TextWidth;
    [SerializeField] private Text TextLenght;
    [SerializeField] private Text TextHeight;

    [SerializeField] private Slider SliderWidth;
    [SerializeField] private Slider SliderLenght;
    [SerializeField] private Slider SliderHeight;
    GameObject instRoom;
   
    public void Start()
    {
        //if (transform.childCount >= 6) DestroyImmediate(transform.GetChild(5).gameObject);
        
        //Spawn();
    }
    public void Spawn()
    {
        width = SliderWidth.value;
        lenght = SliderLenght.value;
        height = SliderHeight.value;

        if (instRoom!=null) DestroyImmediate(instRoom);

        instRoom = Instantiate(roomPref, new Vector3 (0,0,0), Quaternion.Euler(0,0,0));
        instRoom.transform.localScale = new Vector3(width, 1, lenght);

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
        mat.mainTextureScale = new Vector3(1 * (width), 1 * (lenght));
    }

    private void Update()
    {
        TextWidth.text = SliderWidth.value.ToString();
        TextLenght.text = SliderLenght.value.ToString();
        TextHeight.text = SliderHeight.value.ToString();
    }
}
#endif