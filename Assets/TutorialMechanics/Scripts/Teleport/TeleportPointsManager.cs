using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointsManager : MonoBehaviour
{
    [SerializeField] protected List<PointForTeleport> _listPointsForTeleport;

    public void AddPoint(PointForTeleport point)
    {
        if (!_listPointsForTeleport.Contains(point))
        {
            _listPointsForTeleport.Add(point);
            point.SetEnabledPoint(false);
            point.Deactivate();
        }
    }

    public void RemovePoint(PointForTeleport point)
    {
        if (_listPointsForTeleport.Contains(point))
        {
            _listPointsForTeleport.Remove(point);
        }
    }

    public List<PointForTeleport> GetListPoints()
    {
        return new List<PointForTeleport>(_listPointsForTeleport);
    }

    public void ActivatePoints()
    {
        foreach (PointForTeleport point in _listPointsForTeleport)
        {
            point.Activate();
        }
    }

    public void DeactivatePoints()
    {
        foreach (PointForTeleport point in _listPointsForTeleport)
        {
            point.SetEnabledPoint(false);
            point.Deactivate();
        }
    }
}
