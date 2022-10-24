using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace ScenarioSystem
{
    [Serializable]
     public struct Obj
    {
        public int id;
        public float x, y, z, rot;
        public string type;
    }

    [Serializable]
    struct Room
    {
        public float RoomWidth, RoomHeight;
        public float PlayerX, PlayerY, PlayerZ, PlayerRot;
        public Obj[] Objects;
        public Task[] Tasks;
    }

    [Serializable]
    public class Task
    {
        [SerializeField] string name;
        public bool WithPrevious;
        public bool Completed;
        public int Score;
        public ConditionChecker[] Conditions;
        public int TimeLimit; // -1 for no limit
        public int OnTimeout; // 0 - complete with 0 score, 1 - load the latest save;
    }

    [Serializable]
    public class ConditionChecker
    {
        [Serializable]
        public struct Item
        {
            public int Value;
            public string Name;
            public string Condition;    //can be "More", "Less" or "Equal";
        }

        [SerializeField] Item[] items;
        [SerializeField] int[] targetIDs;
        TaskSpecificValues[] targets;
        private ScenarioBehaviour scenario;

        public int Check()
        {
            for (int j = 0; j < targets.Length; j++)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    switch (items[i].Condition)
                    {
                        case ("More"):
                            if (targets[i].Values[items[i].Name] <= items[i].Value)
                            {
                                i = -1;
                                j++;
                                continue;
                            }
                            break;
                        case ("Less"):
                            if (targets[i].Values[items[i].Name] >= items[i].Value)
                            {
                                i = -1;
                                j++;
                                continue;
                            }
                            break;
                        case ("Equal"):
                            if (targets[i].Values[items[i].Name] != items[i].Value)
                            {
                                i = -1;
                                j++;
                                continue;
                            }
                            break;
                    }
                    if(i == items.Length)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public void ConnectToObjectsBase(ScenarioBehaviour connectTo)
        {
            scenario = connectTo;
            targets = new TaskSpecificValues[targetIDs.Length];
            for (int i = 0; i < targetIDs.Length; i++)
            {
                targets[i] = scenario.AccessValues(targetIDs[i]);
            }
        }
    }

}

