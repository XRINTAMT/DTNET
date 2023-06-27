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

    private void Update()
    {
        if (IsNothingInsideCollider())
        {
            Box.LeftTheArea();
        }
    }
}
