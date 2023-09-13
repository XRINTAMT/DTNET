using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableLeftTheAreaTrigger : MonoBehaviour
{
    [SerializeField] InfiniteBox Box;
    private Collider triggerCollider;

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<SpawnableThing>(out _) || other.transform.parent.TryGetComponent<SpawnableThing>(out _))
        {
            Box.LeftTheArea();
        }
    }

    bool IsNothingInsideCollider()
    {
        Collider[] colliders = Physics.OverlapBox(triggerCollider.bounds.center, triggerCollider.bounds.extents, triggerCollider.transform.rotation);
        return colliders.Length == 1; // the trigger itself
    }

    public GameObject ReturnedSpawnable()
    {
        Collider[] colliders = Physics.OverlapBox(triggerCollider.bounds.center, triggerCollider.bounds.extents, triggerCollider.transform.rotation);
        if(colliders.Length > 1)
        {
            for(int i = 1; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetComponentInParent<Packaging>() != null)
                    return colliders[i].gameObject.GetComponentInParent<Packaging>().gameObject;
            }
        }
        return null;
    }

    private void Update()
    {
        if (IsNothingInsideCollider())
        {
            Box.LeftTheArea();
        }
    }
}
