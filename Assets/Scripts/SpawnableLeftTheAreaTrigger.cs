using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableLeftTheAreaTrigger : MonoBehaviour
{
    [SerializeField] InfiniteBox Box;
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<SpawnableThing>(out _) || other.transform.parent.TryGetComponent<SpawnableThing>(out _))
        {
            Box.LeftTheArea();
        }
    }
}
