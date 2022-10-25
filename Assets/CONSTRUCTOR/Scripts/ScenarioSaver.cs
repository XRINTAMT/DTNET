using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioSystem;

public class ScenarioSaver : MonoBehaviour
{
    ObjectsOnTheScene Objects;
    void Start()
    {
        Objects = GetComponent<ObjectsOnTheScene>();
    }

    // Update is called once per frame
    void Update()
    {
        Room SaveFile = new Room();
        SaveFile.Objects = Objects.GetData();
        //Debug.Log(JsonUtility.ToJson(SaveFile));
    }
}
