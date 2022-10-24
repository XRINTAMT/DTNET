using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioSystem;

public class PlacedObjectBehaviour : MonoBehaviour
{
    Obj SaveData;
    public int id;

    void Start()
    {
        SaveData.id = id;
        SaveData.type = gameObject.name.Substring(0, gameObject.name.Length - 7);
    }

    public Obj GetData()
    {
        SaveData.x = transform.position.x;
        SaveData.y = transform.position.y;
        SaveData.z = transform.position.z;
        SaveData.rot = transform.rotation.eulerAngles.y;
        return SaveData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
