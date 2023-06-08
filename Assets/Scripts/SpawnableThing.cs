using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableThing : MonoBehaviour
{
    public InfiniteBox Box;

    public void OnGrabbed()
    {
        Box.ObjectIsTaken(gameObject);
    }
}
