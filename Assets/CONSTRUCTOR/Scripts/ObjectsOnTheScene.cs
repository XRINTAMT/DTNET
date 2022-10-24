using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioSystem;

public class ObjectsOnTheScene : MonoBehaviour
{
    PlacedObjectBehaviour[] objects;
    int iterator = 0;

    void Start()
    {
        objects = new PlacedObjectBehaviour[1024];
    }

    public void Add(PlacedObjectBehaviour obj)
    {
        objects[iterator] = obj;
        obj.id = iterator;
        iterator++;
    }

    public Obj[] GetData()
    {
        Obj[] ObjData = new Obj[iterator];
        for (int i = 0; i < iterator; i++)
        {
            ObjData[i] = objects[i].GetData();
        }
        return ObjData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
