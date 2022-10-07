using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ScenarioSystem
{
    public class TestRoomSerializer : MonoBehaviour
    {
        [SerializeField] GameObject[] ObjectsToSerialize;
        [SerializeField] Task[] Tasks;
        Room Scene;

        void Start()
        {
            Scene.Objects = new Obj[ObjectsToSerialize.Length];
            for (int i = 0; i < ObjectsToSerialize.Length; i++)
            {
                Scene.Objects[i].id = i;
                Scene.Objects[i].x = ObjectsToSerialize[i].transform.position.x;
                Scene.Objects[i].y = ObjectsToSerialize[i].transform.position.y;
                Scene.Objects[i].z = ObjectsToSerialize[i].transform.position.z;
                Scene.Objects[i].rot = ObjectsToSerialize[i].transform.rotation.y;
                Scene.Objects[i].type = ObjectsToSerialize[i].name;
            }
            Scene.RoomHeight = 5;
            Scene.RoomWidth = 7;
            Scene.Tasks = Tasks;
            Debug.Log(JsonUtility.ToJson(Scene));


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

