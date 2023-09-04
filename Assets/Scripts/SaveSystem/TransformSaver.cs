using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : DataSaver
{
    Vector3 LocalPosition;
    Vector3 LocalEulers;
    Vector3 LocalScale;
    Transform Parent;
    bool Kinematic;
    Rigidbody rb;
    bool Saved = false;

    private void Start()
    {
        TryGetComponent<Rigidbody>(out rb);
    }

    public void CopySaves(TransformSaver _from)
    {
        Debug.Log("HEEEEEEEEEEEEEEEEEEY");
        Debug.Log(name + " location saved from " + _from.gameObject.name);
        LocalPosition = _from.LocalPosition;
        LocalEulers = _from.LocalEulers;
        LocalScale = _from.LocalScale;
        Parent = _from.Parent;
        Saved = true;
        Load();
    }

    override public void Save()
    {
        Debug.Log(name + " location saved");
        LocalPosition = transform.localPosition;
        LocalEulers = transform.localEulerAngles;
        LocalScale = transform.localScale;
        Parent = transform.parent;
        Saved = true;
        if (rb != null)
            Kinematic = rb.isKinematic;
    }

    override public void Load()
    {
        if (!Saved)
            return;
        Debug.Log(name + " location loaded");
        transform.parent = Parent;
        transform.localPosition = LocalPosition;
        transform.localEulerAngles = LocalEulers;
        transform.localScale = LocalScale;
        if (rb != null)
            rb.isKinematic = Kinematic;
    }
}
