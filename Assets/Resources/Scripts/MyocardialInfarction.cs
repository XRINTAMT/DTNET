using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class MyocardialInfarction : MonoBehaviour
{
    Scenario MIScenario;

    //Initialization is a little tricky but there's a lot of fields and we better keep them organized like that if we don't want to end up with a mess in the inspector later. 
    //We'll have a lot going on in a real scenario.

    [Serializable] public struct MyocardialInfarctionSubscenariosGroup
    {
        public Scenario HygeneSubscenario;
        public TaskSettings CompleteHygeneSubscenario;
        public Operation HygeneSubscenarioCompleter;

        [Serializable]
        public struct HygeneSubscenarioGroup
        {
            public Scenario WashHandsSubscenario;
            public TaskSettings CompleteWashHandsSubscenario;
            public Operation WashHandsSubscenarioCompleter;
            public UniversalOperation WashHandsSubscenarioMarker;

            [Serializable]
            public struct WashHandsTasksGroup
            {
                public TaskSettings WetHands;
                public TaskSettings GetSoap;
                public UniversalOperation UseSoapMarker;
                public TaskSettings WashSoapAway;
            }
            public WashHandsTasksGroup WashHandsTasks;
        }
        public HygeneSubscenarioGroup HygeneSubscenarios;
    }

    [SerializeField] RoomChanger GoToADifferentRoomOnCompletion;
    [SerializeField] MyocardialInfarctionSubscenariosGroup MyocardialInfarctionScenario;

    void Awake()
    {
        MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario.Task = gameObject.AddComponent<Task>();
        MyocardialInfarctionScenario.CompleteHygeneSubscenario.Task = gameObject.AddComponent<Task>();

        //Wash hands subscenario initialization
        TaskSettings[] washHandsTasks = new TaskSettings[3];
        washHandsTasks[0] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.WetHands;
        washHandsTasks[1] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.GetSoap;
        washHandsTasks[2] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.WashSoapAway;
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.GetSoap.OnCompleted = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.UseSoapMarker;
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario.Task, MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenarioMarker);
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenario = new Scenario(washHandsTasks, MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenarioCompleter);

        //Hygene subscenario initialization
        TaskSettings[] hygeneTasks = new TaskSettings[1];
        hygeneTasks[0] = MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario;
        MyocardialInfarctionScenario.HygeneSubscenarioCompleter = new TaskCompleter(MyocardialInfarctionScenario.CompleteHygeneSubscenario.Task);
        MyocardialInfarctionScenario.HygeneSubscenario = new Scenario(hygeneTasks, MyocardialInfarctionScenario.HygeneSubscenarioCompleter);

        //Main scenario initialization
        TaskSettings[] MITasks = new TaskSettings[1];
        MITasks[0] = MyocardialInfarctionScenario.CompleteHygeneSubscenario;
        MIScenario = new Scenario(MITasks, GoToADifferentRoomOnCompletion);
    }
}
