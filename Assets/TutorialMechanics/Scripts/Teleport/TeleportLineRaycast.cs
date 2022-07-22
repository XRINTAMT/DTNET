using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportLineRaycast : MonoBehaviour
{
    [Header("Teleport")]
    [Tooltip("Объект для телепортации")]
    public GameObject TeleportObject;
    [Tooltip("Можно оставить пустым — используется, если есть контейнер, который нужно телепортировать в дополнение к основному объекту телепорта.")]
    public Transform[] AdditionalTeleports;

    [Header("Aim Settings")]
    [Tooltip("Объект, из которого стреляют лучом")]
    public Transform Aimer;
    [Tooltip("Слои, на которые можно телепортироваться")]
    public LayerMask Layer;
    [Tooltip("Максимальный наклон, по которому вы можете телепортироваться")]
    public float MaxSurfaceAngle = 45;
    [Min(0)]
    public float DistanceMultiplyer = 1;
    [Min(0)]
    public float CurveStrength = 1;
    [Tooltip("Нужно использовать мировые координаты")]
    public LineRenderer Line;
    [Tooltip("Максимальная длина линии телепорта")]
    public int LineSegments = 50;

    [Header("Line Settings")]
    public Gradient CanTeleportColor = new Gradient() { colorKeys = new GradientColorKey[] { new GradientColorKey() { color = Color.green, time = 0 } } };
    public Gradient CantTeleportColor = new Gradient() { colorKeys = new GradientColorKey[] { new GradientColorKey() { color = Color.red, time = 0 } } };

    [Tooltip("Этот игровой объект будет соответствовать положению точки телепорта при прицеливании")]
    public GameObject Indicator;

    [Min(0.0001f)]
    [Tooltip("Расстояние, на котором точка считается целью")]
    public float DistanceCheckPoint = 0.2f;

    public TeleportPointsManager PointsManager
    {
        get
        {
            return _pointsManager;
        }
    }

    [Header("Unity Events")]
    public UnityEvent OnStartTeleport;
    public UnityEvent OnStopTeleport;
    public UnityEvent OnTeleport;

    protected Vector3[] _lineArr;
    protected bool _aiming;
    protected bool _hitting;
    protected RaycastHit _aimHit;
    protected HandTeleportGuard[] _teleportGuards;
    protected AutoHandPlayer _playerBody;
    protected List<PointForTeleport> _listPoints;
    protected TeleportPointsManager _pointsManager;
    protected int _nearestPoint = -1;

    private void Start()
    {
        _pointsManager = FindObjectOfType<TeleportPointsManager>();
        _playerBody = FindObjectOfType<AutoHandPlayer>();
        if (_playerBody != null && _playerBody.transform.gameObject == TeleportObject)
            TeleportObject = null;

        _lineArr = new Vector3[LineSegments];
        _teleportGuards = FindObjectsOfType<HandTeleportGuard>();
    }

    private void Update()
    {
        if (_aiming)
            CalculateTeleport();
        else
            Line.positionCount = 0;

        DrawIndicator();
    }

    private void CalculateTeleport()
    {
        Line.colorGradient = CantTeleportColor;
        var lineList = new List<Vector3>();
        int i;
        _hitting = false;
        for (i = 0; i < LineSegments; i++)
        {
            var time = i / 60f;
            _lineArr[i] = Aimer.transform.position;
            _lineArr[i] += transform.forward * time * DistanceMultiplyer * 15;
            _lineArr[i].y += CurveStrength * (time - Mathf.Pow(9.8f * 0.5f * time, 2));
            lineList.Add(_lineArr[i]);
            if (i != 0)
            {
                if (Physics.Raycast(_lineArr[i - 1], _lineArr[i] - _lineArr[i - 1], out _aimHit, Vector3.Distance(_lineArr[i], _lineArr[i - 1]), ~Hand.GetHandsLayerMask(), QueryTriggerInteraction.Ignore))
                {
                    //Makes sure the angle isnt too steep
                    if (Vector3.Angle(_aimHit.normal, Vector3.up) <= MaxSurfaceAngle && Layer == (Layer | (1 << _aimHit.collider.gameObject.layer)))
                    {
                        int prevNearestPoint = _nearestPoint;
                        _nearestPoint = GetNearestPoint();

                        if (_nearestPoint != prevNearestPoint)
                        {
                            _listPoints?[prevNearestPoint]?.SetEnabledPoint(false);
                        }

                        if (_nearestPoint >= 0)
                        {
                            Line.colorGradient = CanTeleportColor;
                            lineList.Add(_aimHit.point);
                            _hitting = true;
                            _listPoints?[_nearestPoint]?.SetEnabledPoint(true);
                        }
                        break;
                    }
                    break;
                }
            }
        }

        Line.positionCount = i;
        Line.SetPositions(_lineArr);
    }

    private void DrawIndicator()
    {
        if (Indicator != null)
        {
            if (_hitting)
            {
                Indicator.gameObject.SetActive(true);
                Indicator.transform.position = _aimHit.point;
                Indicator.transform.up = _aimHit.normal;
            }
            else
                Indicator.gameObject.SetActive(false);
        }
    }

    private int GetNearestPoint()
    {
        if (_pointsManager == null)
        {
            return -1;
        }

        _listPoints = _pointsManager.GetListPoints();

        PointForTeleport nearestPoint = null;
        float nearestDist = 0f;

        foreach (PointForTeleport point in _listPoints)
        {
            float dist = Vector3.Distance(point.PointPosition, _aimHit.point);
            if (!point.IsLocked && dist <= DistanceCheckPoint)
            {
                if (nearestPoint == null || nearestDist > dist)
                {
                    nearestPoint = point;
                    nearestDist = dist;
                }
            }
        }

        if (nearestPoint == null)
        {
            return -1;
        }
        else
        {
            return _listPoints.IndexOf(nearestPoint);
        }
    }

    public void StartTeleport()
    {
        _aiming = true;
        OnStartTeleport?.Invoke();
    }

    public void CancelTeleport()
    {
        Line.positionCount = 0;
        _hitting = false;
        _aiming = false;
        _nearestPoint = -1;
        OnStopTeleport?.Invoke();
    }

    public void Teleport()
    {
        Queue<Vector3> fromPos = new Queue<Vector3>();
        foreach (var guard in _teleportGuards)
        {
            if (guard.gameObject.activeInHierarchy)
                fromPos.Enqueue(guard.transform.position);
        }

        if (_hitting && _nearestPoint >=0)
        {
            Vector3 targetPos = _listPoints[_nearestPoint].PointPosition;
            if (TeleportObject != null)
            {
                var diff = targetPos - TeleportObject.transform.position;
                TeleportObject.transform.position = targetPos;
                foreach (var teleport in AdditionalTeleports)
                {
                    teleport.position += diff;
                }
            }
            _playerBody?.SetPosition(targetPos);

            OnTeleport?.Invoke();

            foreach (var guard in _teleportGuards)
            {
                if (guard.gameObject.activeInHierarchy)
                {
                    guard.TeleportProtection(fromPos.Dequeue(), guard.transform.position);
                }
            }
        }

        CancelTeleport();
    }
}
