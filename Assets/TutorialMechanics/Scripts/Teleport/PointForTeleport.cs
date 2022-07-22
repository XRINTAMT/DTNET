using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForTeleport : MonoBehaviour
{
    [SerializeField] private MeshRenderer _markerMesh;
    [SerializeField] private GameObject _markerJoint;
    [SerializeField] private Transform _anchor;
    [Header("Materials")]
    [SerializeField] private Material _disableMaterial;
    [SerializeField] private Material _enableMaterial;
    private TeleportPointsManager _pointsManager;

    public bool IsLocked = false;

    public Vector3 PointPosition
    {
        get
        {
            if (_anchor == null)
            {
                return transform.position;
            }
            else
            {
                return _anchor.position;
            }
        }
    }

    private void OnEnable()
    {
        if (_pointsManager == null)
        {
            _pointsManager = FindObjectOfType<TeleportPointsManager>();
        }
        _pointsManager.AddPoint(this);
    }

    private void OnDisable()
    {
        _pointsManager.RemovePoint(this);
    }

    public void Activate()
    {
        _markerMesh.gameObject.SetActive(true);
        _markerJoint.SetActive(true);
    }

    public void Deactivate()
    {
        _markerMesh.gameObject.SetActive(false);
        _markerJoint.SetActive(false);
    }

    public void SetEnabledPoint(bool status)
    {
        if (IsLocked)
        {
            _markerMesh.material = _disableMaterial;
            return;
        }

        if (status)
        {
            _markerMesh.material = _enableMaterial;
        }
        else
        {
            _markerMesh.material = _disableMaterial;
        }
    }

    public void UnlockPoint()
    {
        IsLocked = false;
    }

    public void LockPoint()
    {
        IsLocked = true;
    }
}
