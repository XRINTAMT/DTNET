using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InfiniteBox : DataSaver
{
    [SerializeField] private Transform SpawnOffset;
    [SerializeField] private GameObject ToSpawn;
    [SerializeField] private GameObject SpawnedObject;
    [SerializeField] private bool taken = false;
    [SerializeField] private float ClearanceToSpawn = 3;
    [SerializeField] private SpawnableLeftTheAreaTrigger InTheBox;
    private GameObject SavedSpawnedObject;
    private bool savedTaken;

    public int SpawnedAlready = 0;
    public Action <string,int,string> instNewPackage;


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

        SpawnedObject = null;
        if (PhotonManager.offlineMode)
        {
            SpawnedObject = GameObject.Instantiate(ToSpawn);
            SpawnedObject.transform.position = SpawnOffset.position;
            SpawnedObject.transform.rotation = SpawnOffset.rotation;
            SpawnedObject.GetComponent<SpawnableThing>().Box = this;
            SpawnedObject.GetComponent<ExpirationDate>().Initialize(SpawnedAlready);
            SpawnedAlready += 1;
        }


        if (!PhotonManager.offlineMode)
        {
            if (!PhotonManager._viewerApp)
            {
                SpawnedObject = PhotonNetwork.Instantiate(ToSpawn.name, SpawnOffset.position, SpawnOffset.rotation);

                SpawnedObject.transform.position = SpawnOffset.position;
                SpawnedObject.transform.rotation = SpawnOffset.rotation;
                SpawnedObject.GetComponent<SpawnableThing>().Box = this;
                SpawnedObject.GetComponent<ExpirationDate>().Initialize(SpawnedAlready);
                SpawnedAlready += 1;

                SpawnedObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);

                foreach (PhotonView pv in SpawnedObject.GetComponentsInChildren<PhotonView>())
                {
                    pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                }
                instNewPackage?.Invoke(SpawnedObject.name,SpawnedObject.GetComponent<PhotonView>().ViewID, SpawnedObject.GetComponent<ExpirationDate>().DateStamp.text);
            }
        }


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
        if(SpawnedObject == null)
        {
            if (InTheBox)
                SpawnedObject = InTheBox.ReturnedSpawnable();

            if (SpawnedObject == null)
            {
                taken = false;
                SpawnSpawnable();
            }
        }
    }

    public override void Save()
    {
        SavedSpawnedObject = SpawnedObject;
        savedTaken = taken;
    }

    public override void Load()
    {
        taken = savedTaken;
        if(SavedSpawnedObject != null)
        {
            SpawnedObject = SavedSpawnedObject;
        }
        else
        {
            if (InTheBox)
                SpawnedObject = InTheBox.ReturnedSpawnable();

        }
    }

    public void ObjectDestroyed(GameObject _destroyedObject, GameObject _replacementObject)
    {
        if(_destroyedObject == SavedSpawnedObject)
        {
            SavedSpawnedObject = _replacementObject;
        }
        if(_destroyedObject == SpawnedObject)
        {
            SpawnedObject = _replacementObject;
        }
    }
}
