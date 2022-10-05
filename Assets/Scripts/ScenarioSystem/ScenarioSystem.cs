using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace ScenarioSystem
{
    [Serializable]
    public class Task
    {
        [SerializeField] string name;
        public bool WithPrevious;
        public bool Completed;
        public int Score;
        public ConditionChecker[] Conditions;
    }

    [Serializable]
    public class ConditionChecker
    {
        public struct Item
        {
            public int Value;
            public string Name;
            public string Condition;    //can be "More", "Less" or "Equal";
        }

        [SerializeField] Item[] items;
        [SerializeField] int[] targetIDs;
        TaskSpecificValues[] targets;

        ConditionChecker()
        {
            targets = new TaskSpecificValues[targetIDs.Length];
            for (int i = 0; i < targetIDs.Length; i++){
                //find objects and get their TaskSpecificValues
            }
        }

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
        
    }

}

