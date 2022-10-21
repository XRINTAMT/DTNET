using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace ScenarioSystem
{
    [Serializable]
    struct Obj
    {
        public int id;
        public float x, y, z, rot;
        public string type;
        public CustomField[] ObjectSpecificValues;
    }

    [Serializable]
    struct CustomField
    {
        public string name;
        public int value;
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
    class Task
    {
        public string name;
        public bool WithPrevious;
        public bool Completed;
        public int Score;
        public ConditionChecker[] Conditions;
        public Command[] OnComplete;
        public int TimeLimit; // -1 for no limit
        public int OnTimeout; // 0 - complete with 0 score, 1 - load the latest save
    }

    [Serializable]
    class Command
    {
        public CustomField[] Settings;
        public int ObjectID;
        private ScenarioBehaviour scenario;
        private TaskSpecificValues data;
        public void ConnectToObjectsBase(ScenarioBehaviour connectTo)
        {
            scenario = connectTo;
            data = scenario.AccessValues(ObjectID);
        }

        public void Completed()
        {
            for(int i = 0; i < Settings.Length; i++)
            {
                data.SendDataSystem(Settings[i].name, Settings[i].value);
            }
        }
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

        public bool Check()
        {
            for (int j = 0; j < targets.Length; j++)
            {
                //Debug.Log("Checking values on " + targets[j].name);
                int i;
                for (i = 0; i < items.Length; i++)
                {
                    int value = -1;
                    targets[j].TryGetSystem(items[i].Name, ref value);
                    //Debug.Log("Checking " + items[i].Name);
                    switch (items[i].Condition)
                    {
                        case ("More"):
                            if (value > items[i].Value)
                            {
                                continue;
                            }
                            break;
                        case ("Less"):
                            if (value < items[i].Value)
                            {
                                continue;
                            }
                            break;
                        case ("Equal"):
                            if (value == items[i].Value)
                            {
                                continue;
                            }
                            break;
                        case ("NotEqual"):
                            if (value != items[i].Value)
                            {
                                continue;
                            }
                            break;
                    }
                    //Debug.Log(items[i].Name + " did not meet the requirements");
                    //Debug.Log(items[i].Name + " is " + value + " but should be " + items[i].Condition + " " + items[i].Value);
                    break;
                }
                if (i == items.Length)
                {
                    return true;
                }
            }
            return false;
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

