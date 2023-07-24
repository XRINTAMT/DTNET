using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBox : MonoBehaviour
{
    [SerializeField] Transform SpawnOffset;
    [SerializeField] GameObject ToSpawn;
    [SerializeField] GameObject SpawnedObject;
    [SerializeField] bool taken = false;
    [SerializeField] float ClearanceToSpawn = 3;
    public int SpawnedAlready = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(SpawnedObject == null)
        {
            SpawnSpawnable();
        }
    }

    private GameObject SpawnSpawnable()
    {
        SpawnedObject = GameObject.Instantiate(ToSpawn);
        SpawnedObject.transform.position = SpawnOffset.position;
        SpawnedObject.transform.rotation = SpawnOffset.rotation;
        SpawnedObject.GetComponent<SpawnableThing>().Box = this;
        SpawnedObject.GetComponent<ExpirationDate>().Initialize(SpawnedAlready);
        SpawnedAlready += 1;
        return SpawnedObject;
    }

    public void ObjectIsTaken(GameObject _obj)
    {
        if(_obj == SpawnedObject)
        {
            taken = true;
        }
    }

    public void LeftTheArea()
    {
        if (taken)
        {
            taken = false;
            SpawnSpawnable();
        }
    }

    private void Update()
    {
        /*
        if (taken)
        {
            Debug.Log((SpawnedObject.transform.position - SpawnOffset.position).magnitude);
            if ((SpawnedObject.transform.position - SpawnOffset.position).magnitude > ClearanceToSpawn)
            {
                taken = false;
                SpawnSpawnable();
            }
        }
        */
    }
}
