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
        //All of this scenarios tasks here

        //Hygene subscenario tasks here
        public Scenario HygeneSubscenario;
        [NonSerialized] public TaskSettings CompleteHygeneSubscenario;
        public Operation HygeneSubscenarioCompleter;
        public UniversalOperation HygeneSubscenarioMarker;

        [Serializable]
        public struct HygeneSubscenarioGroup
        {
            public Scenario WashHandsSubscenario;
            [NonSerialized] public TaskSettings CompleteWashHandsSubscenario;
            public Operation WashHandsSubscenarioCompleter;
            public UniversalOperation WashHandsSubscenarioMarker;

            [Serializable]
            public struct WashHandsTasksGroup
            {
                public TaskSettings WetHands;
                public TaskSettings GetSoap;
                public TaskSettings WashSoapAway;
            }
            public WashHandsTasksGroup WashHandsTasks;
        }
        public HygeneSubscenarioGroup HygeneSubscenarios;

        //Check patient subscenario tasks here
        public Scenario CheckPatientSubscenario;
        [NonSerialized] public TaskSettings CompleteCheckPatientSubscenario;
        public Operation CheckPatientSubscenarioCompleter;
        public UniversalOperation CheckPatientSubscenarioMarker;

        [Serializable]
        public struct CheckPatientSubscenarioGroup
        {
            public TaskSettings DialogueOne;
            public UniversalOperation DialogueOneMarker;
            public TaskSettings ConnectTheMonitor;
            public UniversalOperation ConnectTheMonitorMarker;
            public TaskSettings FillObservationSheet;
            public UniversalOperation FillObservationSheetMarker;
            public TaskSettings ReadDoctorsAppointments;
            public UniversalOperation ReadDoctorsAppointmentsMarker;
        }
        public CheckPatientSubscenarioGroup CheckPatientSubscenarios;

        //Injection subscenario tasks here
        public Scenario InjectionSubscenario;
        [NonSerialized] public TaskSettings CompleteInjectionSubscenario;
        public Operation InjectionSubscenarioCompleter;
        public UniversalOperation InjectionSubscenarioMarker;

        [Serializable]
        public struct InjectionSubscenarioGroup
        {
            public TaskSettings DiluteDopamine;
            public UniversalOperation DiluteDopamineMarker;
            public TaskSettings ConnectPumps;
            public UniversalOperation ConnectPumpsMarker;
            public TaskSettings MakeAnInjection;
            public UniversalOperation MakeAnInjectionMarker;
        }
        public InjectionSubscenarioGroup InjectionSubscenarios;

        //Keep monitoring subscenario tasks here
        public Scenario KeepMonitoringSubscenario;
        [NonSerialized] public TaskSettings CompleteKeepMonitoringSubscenario;
        public Operation KeepMonitoringSubscenarioCompleter;
        public UniversalOperation KeepMonitoringSubscenarioMarker;

        [Serializable]
        public struct KeepMonitoringSubscenarioGroup
        {
            public TaskSettings FillObservationSheet;
            public UniversalOperation FillObservationSheetMarker;
            public TaskSettings DialogueTwo;
            public UniversalOperation DialogueTwoMarker;
            public TaskSettings AlarmButton;
            public UniversalOperation AlarmButtonMarker;
        }
        public KeepMonitoringSubscenarioGroup KeepMonitoringSubscenarios;
    }

    [SerializeField] RoomChanger GoToADifferentRoomOnCompletion;
    [SerializeField] MyocardialInfarctionSubscenariosGroup MyocardialInfarctionScenario;

    void Awake()
    {
        MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario.Task = gameObject.AddComponent<Task>();
        MyocardialInfarctionScenario.CompleteHygeneSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.CompleteHygeneSubscenario.Task = gameObject.AddComponent<Task>();
        MyocardialInfarctionScenario.CompleteCheckPatientSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.CompleteCheckPatientSubscenario.Task = gameObject.AddComponent<Task>();
        MyocardialInfarctionScenario.CompleteInjectionSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.CompleteInjectionSubscenario.Task = gameObject.AddComponent<Task>();
        MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario.Task = gameObject.AddComponent<Task>();

        //Wash hands subscenario initialization
        TaskSettings[] washHandsTasks = new TaskSettings[3];
        washHandsTasks[0] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.WetHands;
        washHandsTasks[1] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.GetSoap;
        washHandsTasks[2] = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.WashSoapAway;
        //MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.GetSoap.OnCompleted = MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsTasks.UseSoapMarker;
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario.Task, MyocardialInfarctionScenario.HygeneSubscenarioMarker);
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenario = new Scenario(washHandsTasks, MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenarioCompleter);

        //Hygene subscenario initialization
        TaskSettings[] hygeneTasks = new TaskSettings[1];
        hygeneTasks[0] = MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario;
        MyocardialInfarctionScenario.HygeneSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteHygeneSubscenario.Task, MyocardialInfarctionScenario.HygeneSubscenarioMarker);
        MyocardialInfarctionScenario.HygeneSubscenario = new Scenario(hygeneTasks, MyocardialInfarctionScenario.HygeneSubscenarioCompleter);

        //Check patient subcsenario initialization
        TaskSettings[] checkPatientTasks = new TaskSettings[4];
        checkPatientTasks[0] = MyocardialInfarctionScenario.CheckPatientSubscenarios.DialogueOne;
        checkPatientTasks[0].OnCompleted = MyocardialInfarctionScenario.CheckPatientSubscenarios.DialogueOneMarker;
        checkPatientTasks[1] = MyocardialInfarctionScenario.CheckPatientSubscenarios.ConnectTheMonitor;
        checkPatientTasks[1].OnCompleted = MyocardialInfarctionScenario.CheckPatientSubscenarios.ConnectTheMonitorMarker;
        checkPatientTasks[2] = MyocardialInfarctionScenario.CheckPatientSubscenarios.FillObservationSheet;
        checkPatientTasks[2].OnCompleted = MyocardialInfarctionScenario.CheckPatientSubscenarios.FillObservationSheetMarker;
        checkPatientTasks[3] = MyocardialInfarctionScenario.CheckPatientSubscenarios.ReadDoctorsAppointments;
        checkPatientTasks[3].OnCompleted = MyocardialInfarctionScenario.CheckPatientSubscenarios.ReadDoctorsAppointmentsMarker;
        MyocardialInfarctionScenario.CheckPatientSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteCheckPatientSubscenario.Task, MyocardialInfarctionScenario.CheckPatientSubscenarioMarker);
        MyocardialInfarctionScenario.CheckPatientSubscenario = new Scenario(checkPatientTasks, MyocardialInfarctionScenario.CheckPatientSubscenarioCompleter);

        //Check patient subcsenario initialization
        TaskSettings[] injectionTasks = new TaskSettings[3];
        injectionTasks[0] = MyocardialInfarctionScenario.InjectionSubscenarios.DiluteDopamine;
        injectionTasks[0].OnCompleted = MyocardialInfarctionScenario.InjectionSubscenarios.DiluteDopamineMarker;
        injectionTasks[1] = MyocardialInfarctionScenario.InjectionSubscenarios.ConnectPumps;
        injectionTasks[1].OnCompleted = MyocardialInfarctionScenario.InjectionSubscenarios.ConnectPumpsMarker;
        injectionTasks[2] = MyocardialInfarctionScenario.InjectionSubscenarios.MakeAnInjection;
        injectionTasks[2].OnCompleted = MyocardialInfarctionScenario.InjectionSubscenarios.MakeAnInjectionMarker;
        MyocardialInfarctionScenario.InjectionSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteInjectionSubscenario.Task, MyocardialInfarctionScenario.InjectionSubscenarioMarker);
        MyocardialInfarctionScenario.InjectionSubscenario = new Scenario(injectionTasks, MyocardialInfarctionScenario.InjectionSubscenarioCompleter);

        //Check patient subcsenario initialization
        TaskSettings[] keepMonitoringTasks = new TaskSettings[3];
        keepMonitoringTasks[0] = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.FillObservationSheet;
        keepMonitoringTasks[0].OnCompleted = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.FillObservationSheetMarker;
        keepMonitoringTasks[1] = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.DialogueTwo;
        keepMonitoringTasks[1].OnCompleted = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.DialogueTwoMarker;
        keepMonitoringTasks[2] = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.AlarmButton;
        keepMonitoringTasks[2].OnCompleted = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.AlarmButtonMarker;
        MyocardialInfarctionScenario.KeepMonitoringSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario.Task, MyocardialInfarctionScenario.KeepMonitoringSubscenarioMarker);
        MyocardialInfarctionScenario.KeepMonitoringSubscenario = new Scenario(keepMonitoringTasks, MyocardialInfarctionScenario.KeepMonitoringSubscenarioCompleter);

        //Main scenario initialization
        TaskSettings[] MITasks = new TaskSettings[4];
        MITasks[0] = MyocardialInfarctionScenario.CompleteHygeneSubscenario;
        MITasks[1] = MyocardialInfarctionScenario.CompleteCheckPatientSubscenario; 
        MITasks[2] = MyocardialInfarctionScenario.CompleteInjectionSubscenario;
        MITasks[3] = MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario;
        MIScenario = new Scenario(MITasks, GoToADifferentRoomOnCompletion);
    }
}
