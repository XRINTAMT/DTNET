using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCharacterFollow : MonoBehaviour
{

    public Transform player;
    public Transform defaultPos;
    public Transform m_trLookAt = null;
    Transform m_Transform;
    Vector3 m_vecInitPosition;
    Vector3 m_vecInitEuler;
    public float m_LookAtWeight = 0;
    [SerializeField] protected Animator m_Animator;
    public bool inArea;
    public float interpolationSpeed;
    Vector3 startPos;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        if (!Animator)
            Animator = GetComponent<Animator>();
        m_Transform = transform;
        m_vecInitEuler = m_Transform.localEulerAngles;
        m_vecInitPosition = m_Transform.localPosition;
        m_trLookAt = defaultPos;
        inArea = false;

        startPos = defaultPos.position;
        startRot = defaultPos.rotation;
    }
    private void Update()
    {

        if (defaultPos.position != startPos)
        {
            defaultPos.position = Vector3.MoveTowards(defaultPos.position, startPos, interpolationSpeed * Time.deltaTime);
        }
        if (defaultPos.rotation != startRot)
        {
            defaultPos.rotation = Quaternion.RotateTowards(defaultPos.rotation, startRot, 10*interpolationSpeed * Time.deltaTime);
        }

    }
    public void ExitArea() 
    {
        defaultPos.position = player.position;
        defaultPos.rotation = player.rotation;
        inArea = false;
    }
    public void EnterArea()
    {
        m_LookAtWeight = 0;
        inArea = true;
      
    }
    public Animator Animator
    {
        get => m_Animator;
        set => m_Animator = value;
    }
    void OnAnimatorIK(int layerIndex)
    {
        if (inArea) m_trLookAt = player;

        if (!inArea) m_trLookAt = defaultPos;
        
      
        if (!Animator)
            return;
        if (m_trLookAt == null)
        {
            _StopLookAt();
            return;
        }
        _StartLookAt(m_trLookAt.position);
    }
   
    void _StartLookAt(Vector3 lookPos)
    {
        m_LookAtWeight = Mathf.Clamp(m_LookAtWeight + 0.01f, 0, 1);
        Animator.SetLookAtWeight(m_LookAtWeight);
        Animator.SetLookAtPosition(lookPos);
    }
    void _StopLookAt()
    {
        m_Transform.localPosition = m_vecInitPosition;
        m_Transform.localEulerAngles = m_vecInitEuler;
        m_LookAtWeight = Mathf.Clamp(m_LookAtWeight - 0.01f, 0, 1);
        Animator.SetLookAtWeight(m_LookAtWeight);
    }
}
