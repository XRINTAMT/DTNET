using Autohand.Demo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TeleportButtonHandler : MonoBehaviour
{
    public TeleportLineRaycast TeleportLine;
    public XRNode Role;
    public CommonButton Button;

    protected bool _teleporting = false;
    protected InputDevice _device;
    protected List<InputDevice> _devices;

    void Start()
    {
        _devices = new List<InputDevice>();
    }

    void FixedUpdate()
    {
        InputDevices.GetDevicesAtXRNode(Role, _devices);
        if (_devices.Count > 0)
            _device = _devices[0];

        if (_device != null && _device.isValid)
        {
            //Sets hand fingers wrap
            if (_device.TryGetFeatureValue(XRHandControllerLink.GetCommonButton(Button), out bool teleportButton))
            {
                if (_teleporting && !teleportButton)
                {
                    TeleportLine.Teleport();
                    _teleporting = false;
                    TeleportLine.PointsManager.DeactivatePoints();
                }
                else if (!_teleporting && teleportButton)
                {
                    TeleportLine.PointsManager.ActivatePoints();
                    TeleportLine.StartTeleport();
                    _teleporting = true; 
                }
            }
        }
    }
}
