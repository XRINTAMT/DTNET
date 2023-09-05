using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTick : DataSaver
{
    public Vector3 LocalPosition;
    bool Saved = false;

    private void Awake()
    {
        //LocalPosition = transform.localPosition;
        Debug.Log(gameObject.name + " pos is " + LocalPosition);
    }

    override public void Save()
    {

    }

    override public void Load()
    {
        //transform.localPosition = LocalPosition;
    }
}
