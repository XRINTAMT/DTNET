using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InfiniteBox : MonoBehaviour
{
    [SerializeField] Transform SpawnOffset;
    [SerializeField] GameObject ToSpawn;
    [SerializeField] GameObject SpawnedObject;
    [SerializeField] bool taken = false;
    [SerializeField] float ClearanceToSpawn = 3;
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
        if (PhotonManager.offlineMode)
        {
            SpawnedObject = GameObject.Instantiate(ToSpawn);
        }


        if (!PhotonManager.offlineMode)
        {
            SpawnedObject = PhotonNetwork.Instantiate("Tubing(Packaged)Photon", SpawnOffset.position, SpawnOffset.rotation);
            if (PhotonManager._viewerApp)
            {
                foreach (Rigidbody rb in SpawnedObject.GetComponentsInChildren<Rigidbody>())
                {
                    rb.isKinematic = true;
                }
            }

            if (!PhotonManager._viewerApp)
            {
                foreach (PhotonView pv in SpawnedObject.GetComponentsInChildren<PhotonView>())
                {
                    pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                }
            }
        }

        SpawnedObject.transform.position = SpawnOffset.position;
        SpawnedObject.transform.rotation = SpawnOffset.rotation;
        SpawnedObject.GetComponent<SpawnableThing>().Box = this;
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
