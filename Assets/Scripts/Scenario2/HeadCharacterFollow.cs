using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCharacterFollow : MonoBehaviour
{

    public Transform player;
    public Transform defaultPos;
    Transform m_trLookAt = null;
    Transform m_Transform;
    Vector3 m_vecInitPosition;
    Vector3 m_vecInitEuler;
    float m_LookAtWeight = 0;
    [SerializeField] protected Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        if (!Animator)
            Animator = GetComponent<Animator>();
        m_Transform = transform;
        m_vecInitEuler = m_Transform.localEulerAngles;
        m_vecInitPosition = m_Transform.localPosition;
        m_trLookAt = defaultPos;
    }
    public void ExitArea() 
    {
        m_trLookAt = defaultPos;
    }
    public void EnterArea()
    {
        m_trLookAt = player;
    }
    public Animator Animator
    {
        get => m_Animator;
        set => m_Animator = value;
    }
    void OnAnimatorIK(int layerIndex)
    {
        m_trLookAt = player;
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
