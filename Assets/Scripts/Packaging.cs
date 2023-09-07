using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Photon.Pun;
using System;

public class Packaging : DataSaver
{
    public Grabbable Package;
    public Grabbable RemovablePart;
    public Grabbable Content;
    public Action<string, int, string> instNewPackage;
    public string ThisWhole;
    public string ThisRipped;

    private Packaging Replacement;

    bool SavedDisabled;

    bool isUnpacked;
    bool isEjected;
    bool savedIsUnpacked;
    bool savedIsEjected;
    bool savedExists;

    public void Start()
    {
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        savedExists = false;
        savedIsEjected = false;
        savedIsUnpacked = false;
    }

    public void OnUnpacked()
    {
        //Content.gameObject.AddComponent<Rigidbody>();
        Content.enabled = true;
        isUnpacked = true;
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        _rb.detectCollisions = true;
        _rb.angularDrag = 0.05f;
        _rb.mass = 1;
        RemovablePart.gameObject.AddComponent<Trash>();
    }

    public void OnEjected()
    {
        isEjected = true;
        Debug.Log("Ejected");
        Package.gameObject.AddComponent<Trash>();
    }

    public void OnGrabbed()
    {
        if(RemovablePart != null)
        {
            RemovablePart.enabled = true;
        }
        if (!isUnpacked)
        {
            Rigidbody _rb = Content.GetComponent<Rigidbody>();
            _rb.detectCollisions = false;
            _rb.angularDrag = 0;
            _rb.mass = 0;
        }
    }

    public void OnContentGrabbed()
    {
        ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = 1000;
        }
    }

    public void OnContentReleased()
    {
        if (!isEjected)
        {
            ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
            if (CJ != null)
            {
                CJ.breakForce = float.PositiveInfinity;
            }
        }
    }


    public void OnRemovableGrabbed()
    {
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = 2500;
        }
    }

    public void OnReleased()
    {
        if(RemovablePart != null)
        {
            RemovablePart.enabled = isUnpacked;
        }
        if (!isUnpacked)
        {
            Rigidbody _rb = Content.GetComponent<Rigidbody>();
            _rb.detectCollisions = true;
            _rb.angularDrag = 0.05f;
            _rb.mass = 1;
        }
        
    }

    public void OnRemovableReleased()
    {
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = float.PositiveInfinity;
        }
    }

    public void MainPackagingDestroyed()
    {
        if (!isUnpacked)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!isEjected)
            {
                if (Content != null)
                {
                    Destroy(Content.gameObject);
                }

            }
        }
    }

    public void MainPackagingDisabled()
    {
        if (!isUnpacked)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (!isEjected)
            {
                if (Content != null)
                {
                    Content.gameObject.SetActive(false);
                }

            }
        }
    }

    public GameObject SpawnCopyPUN(string ToSpawn)
    {
        GameObject _spawnedObject = null;
        if (PhotonManager.offlineMode)
        {
            //_spawnedObject = GameObject.Instantiate(ToSpawn);
            _spawnedObject = GameObject.Instantiate(Resources.Load(ToSpawn) as GameObject);

            _spawnedObject.transform.position = transform.position;
            _spawnedObject.transform.rotation = transform.rotation;
            _spawnedObject.GetComponent<SpawnableThing>().Box = GetComponent<SpawnableThing>().Box;
            _spawnedObject.GetComponent<ExpirationDate>().InitCopy(GetComponent<ExpirationDate>());
        }
        else
        {
            if (!PhotonManager._viewerApp)
            {
                _spawnedObject = PhotonNetwork.Instantiate(ToSpawn, transform.position, transform.rotation);

                _spawnedObject.transform.position = transform.position;
                _spawnedObject.transform.rotation = transform.rotation;
                _spawnedObject.GetComponent<SpawnableThing>().Box = GetComponent<SpawnableThing>().Box;
                _spawnedObject.GetComponent<ExpirationDate>().InitCopy(GetComponent<ExpirationDate>());

                _spawnedObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);

                foreach (PhotonView pv in _spawnedObject.GetComponentsInChildren<PhotonView>())
                {
                    pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                }
                instNewPackage?.Invoke(_spawnedObject.name, _spawnedObject.GetComponent<PhotonView>().ViewID, _spawnedObject.GetComponent<ExpirationDate>().DateStamp.text);
            }
        }

        return _spawnedObject;
    }

    void SyncSpawnedTransforms(GameObject _spawnedObject)
    {
        Debug.Log("Syncing...");
        Packaging _pkg = _spawnedObject.GetComponent<Packaging>();
        _pkg.Package.GetComponent<TransformSaver>().CopySaves(Package.GetComponent<TransformSaver>());
        _pkg.Content.GetComponent<TransformSaver>().CopySaves(Content.GetComponent<TransformSaver>());
        if (_pkg.RemovablePart != null)
        {
            _pkg.RemovablePart.GetComponent<TransformSaver>().CopySaves(RemovablePart.GetComponent<TransformSaver>());
        }
    }

    public override void Save()
    {
        savedExists = gameObject.activeSelf;
        savedIsEjected = isEjected;
        savedIsUnpacked = isUnpacked;
    }

    public override void Load()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        if (!savedExists)
        {
            if (isUnpacked)
            {
                Destroy(RemovablePart.gameObject);
                if (isEjected)
                {
                    Destroy(Content.gameObject);
                }
            }
            Destroy(this.gameObject);
            yield break;
        }
        if (!savedIsUnpacked && isUnpacked && !isEjected)
        {
            GameObject _spawnedCopy = SpawnCopyPUN(ThisWhole);
            yield return new WaitForSeconds(0.1f);
            SyncSpawnedTransforms(_spawnedCopy);
            Destroy(RemovablePart.gameObject);
            Destroy(gameObject);
            yield break;
        }
        if (!savedIsEjected && isEjected)
        {
            GameObject _spawnedCopy;
            if (savedIsUnpacked)
            {
                _spawnedCopy = SpawnCopyPUN(ThisRipped);
            }
            else
            {
                _spawnedCopy = SpawnCopyPUN(ThisWhole);
            }
            yield return new WaitForSeconds(0.1f);
            SyncSpawnedTransforms(_spawnedCopy);
            Destroy(RemovablePart.gameObject);
            Destroy(Content.gameObject);
            Destroy(gameObject);
            yield break;
        }
    }
}
