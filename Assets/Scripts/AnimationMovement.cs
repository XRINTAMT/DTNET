using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
[ExecuteInEditMode]
public class AnimationMovement : MonoBehaviour
{
    [SerializeField] private Transform moveObj;
    [SerializeField] private float speed = 1;
    [SerializeField] List<Transform> MoveListPonints = new List<Transform>();    

    Transform nextPoint;
    int indexPoint;

    public UnityEvent OnStartMove;
    public UnityEvent OnFinishMove;
    public bool startMoving;
    public Action walk;
    bool startEvent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            MoveListPonints.Add(child);
        }
    }

    private void Move() 
    {

        nextPoint = MoveListPonints[indexPoint];

        Vector3 newDirection = Vector3.RotateTowards(moveObj.forward, nextPoint.position - moveObj.position, speed * Time.deltaTime, 10f);
        moveObj.rotation = Quaternion.LookRotation(newDirection);
        moveObj.position = Vector3.MoveTowards(moveObj.position, nextPoint.position, speed * Time.deltaTime);
       
        if (moveObj.position == nextPoint.position)
        {
            if (indexPoint <= MoveListPonints.Count - 1)
            {
                indexPoint++;
            }
        }

        if (moveObj.position == MoveListPonints[MoveListPonints.Count - 1].position)
        {
            OnFinishMove.Invoke();
            startMoving = false;
            startEvent = false;
        }

    }

    public void StartMove(bool startMoving) 
    {
        this.startMoving = startMoving;
        walk?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            if (!startEvent)
            {
                OnStartMove.Invoke();
                startEvent = true;
            }
            Move(); 
        }

    }
}

//[CustomEditor(typeof(AnimationMovement))]
//class DecalMeshHelper : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        AnimationMovement animationMovement= (AnimationMovement)target;
//        if (GUILayout.Button("StartMove")) 
//        {
//            animationMovement.StartMove(true);
//        }
//    }
//}
