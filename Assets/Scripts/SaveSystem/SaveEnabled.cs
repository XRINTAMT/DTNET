using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEnabled : DataSaver
{
    bool GameObjActive;

    public override void Load()
    {
        Debug.Log("setting " + gameObject.name + " to " + (GameObjActive ? "active" : "disabled"));
        gameObject.SetActive(GameObjActive);
    }

    public override void Save()
    {
        GameObjActive = gameObject.activeSelf;
    }
}
