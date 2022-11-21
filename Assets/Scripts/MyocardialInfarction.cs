using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenarioTaskSystem;

public class MyocardialInfarction : MonoBehaviour
{
    Scenario MIScenario;
    bool GuidedMode;
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

            public TaskSettings PutGlovesOn;
            public UniversalOperation PutGlovesOnSubscenarioMarker;
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
        }
        public KeepMonitoringSubscenarioGroup KeepMonitoringSubscenarios;

        //Cardiac arrest handling tasks here
        
        public Scenario CardiacArrestSubscenario;
        [NonSerialized] public TaskSettings CompleteCardiacArrestSubscenario;
        public Operation CardiacArrestSubscenarioCompleter;
        public UniversalOperation CardiacArrestSubscenarioMarker;

        [Serializable]
        public struct CardiacArrestSubscenarioGroup
        {
            public TaskSettings AlarmButton;
            public UniversalOperation AlarmButtonMarker;
            public TaskSettings CallDoctor;
            public UniversalOperation CallDoctorMarker;
            public TaskSettings ApplyStimPads;
            public UniversalOperation ApplyStimPadsMarker;
            public TaskSettings ReportToDoc;
            public UniversalOperation ReportToDocMarker;
            public TaskSettings PositionThePatient;
            public UniversalOperation PositionThePatientMarker;
            public TaskSettings ReportSpO2;
            public UniversalOperation ReportSpO2Marker;
            public TaskSettings MakeAnInjection;
            public UniversalOperation MakeAnInjectionMarker;
            public TaskSettings KeepRegulating;
            public UniversalOperation KeepRegulatingMarker;
            public TaskSettings DialogueThree;
            public UniversalOperation DialogueThreeMarker;
        }
        public CardiacArrestSubscenarioGroup CardiacArrestSubscenarios;
    }

    [SerializeField] RoomChanger GoToADifferentRoomOnCompletion;
    [SerializeField] MyocardialInfarctionSubscenariosGroup MyocardialInfarctionScenario;

    void Awake()
    {
        GuidedMode = PlayerPrefs.GetInt("GuidedMode", 1) == 1;

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
        MyocardialInfarctionScenario.CompleteCardiacArrestSubscenario = new TaskSettings();
        MyocardialInfarctionScenario.CompleteCardiacArrestSubscenario.Task = gameObject.AddComponent<Task>();

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
        TaskSettings[] hygeneTasks = new TaskSettings[2];
        hygeneTasks[0] = MyocardialInfarctionScenario.HygeneSubscenarios.CompleteWashHandsSubscenario;
        MyocardialInfarctionScenario.HygeneSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteHygeneSubscenario.Task, MyocardialInfarctionScenario.HygeneSubscenarioMarker);
        hygeneTasks[1] = MyocardialInfarctionScenario.HygeneSubscenarios.PutGlovesOn;
        hygeneTasks[1].Order = hygeneTasks[0].Task;
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
        TaskSettings[] keepMonitoringTasks = new TaskSettings[2];
        keepMonitoringTasks[0] = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.DialogueTwo;
        keepMonitoringTasks[0].OnCompleted = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.DialogueTwoMarker;
        keepMonitoringTasks[1] = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.FillObservationSheet;
        keepMonitoringTasks[1].OnCompleted = MyocardialInfarctionScenario.KeepMonitoringSubscenarios.FillObservationSheetMarker;
        MyocardialInfarctionScenario.KeepMonitoringSubscenarioCompleter = 
            new TaskCompleter(MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario.Task, MyocardialInfarctionScenario.KeepMonitoringSubscenarioMarker);
        MyocardialInfarctionScenario.KeepMonitoringSubscenario = new Scenario(keepMonitoringTasks, MyocardialInfarctionScenario.KeepMonitoringSubscenarioCompleter);

        //Check patient subcsenario initialization
        TaskSettings[] cardiacArrestTasks = new TaskSettings[9];
        cardiacArrestTasks[0] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.AlarmButton;
        cardiacArrestTasks[0].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.AlarmButtonMarker;
        cardiacArrestTasks[1] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.CallDoctor;
        cardiacArrestTasks[1].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.CallDoctorMarker;
        cardiacArrestTasks[2] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ApplyStimPads;
        cardiacArrestTasks[2].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ApplyStimPadsMarker;
        cardiacArrestTasks[3] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ReportToDoc;
        cardiacArrestTasks[3].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ReportToDocMarker;
        cardiacArrestTasks[4] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.PositionThePatient;
        cardiacArrestTasks[4].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.PositionThePatientMarker;
        cardiacArrestTasks[5] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ReportSpO2;
        cardiacArrestTasks[5].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.ReportSpO2Marker;
        cardiacArrestTasks[6] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.MakeAnInjection;
        cardiacArrestTasks[6].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.MakeAnInjectionMarker;
        cardiacArrestTasks[7] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.KeepRegulating;
        cardiacArrestTasks[7].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.KeepRegulatingMarker;
        cardiacArrestTasks[8] = MyocardialInfarctionScenario.CardiacArrestSubscenarios.DialogueThree;
        cardiacArrestTasks[8].OnCompleted = MyocardialInfarctionScenario.CardiacArrestSubscenarios.DialogueThreeMarker;

        MyocardialInfarctionScenario.CardiacArrestSubscenarioCompleter =
            new TaskCompleter(MyocardialInfarctionScenario.CompleteCardiacArrestSubscenario.Task, MyocardialInfarctionScenario.CardiacArrestSubscenarioMarker);
        MyocardialInfarctionScenario.CardiacArrestSubscenario = new Scenario(cardiacArrestTasks, MyocardialInfarctionScenario.CardiacArrestSubscenarioCompleter);

        //Main scenario initialization
        TaskSettings[] MITasks = new TaskSettings[5];
        MITasks[0] = MyocardialInfarctionScenario.CompleteHygeneSubscenario;
        MITasks[1] = MyocardialInfarctionScenario.CompleteCheckPatientSubscenario; 
        MITasks[2] = MyocardialInfarctionScenario.CompleteInjectionSubscenario;
        MITasks[3] = MyocardialInfarctionScenario.CompleteKeepMonitoringSubscenario;
        MITasks[4] = MyocardialInfarctionScenario.CompleteCardiacArrestSubscenario;
        MIScenario = new Scenario(MITasks, GoToADifferentRoomOnCompletion);
        MyocardialInfarctionScenario.HygeneSubscenarioCompleter.Include(
            new ScenarioActivator(MyocardialInfarctionScenario.CheckPatientSubscenario));
        MyocardialInfarctionScenario.CheckPatientSubscenarioCompleter.Include(
            new ScenarioActivator(MyocardialInfarctionScenario.InjectionSubscenario));
        MyocardialInfarctionScenario.InjectionSubscenarioCompleter.Include(
            new ScenarioActivator(MyocardialInfarctionScenario.KeepMonitoringSubscenario));
        MyocardialInfarctionScenario.KeepMonitoringSubscenarioCompleter.Include(
            new ScenarioActivator(MyocardialInfarctionScenario.CardiacArrestSubscenario));
        MyocardialInfarctionScenario.HygeneSubscenario.Activate();
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenario.Activate();

        MyocardialInfarctionScenario.CardiacArrestSubscenario.SetGuidedMode(GuidedMode);
        MyocardialInfarctionScenario.CheckPatientSubscenario.SetGuidedMode(GuidedMode);
        MyocardialInfarctionScenario.KeepMonitoringSubscenario.SetGuidedMode(GuidedMode);
        MyocardialInfarctionScenario.InjectionSubscenario.SetGuidedMode(GuidedMode);
        MyocardialInfarctionScenario.HygeneSubscenario.SetGuidedMode(GuidedMode);
        MyocardialInfarctionScenario.HygeneSubscenarios.WashHandsSubscenario.SetGuidedMode(GuidedMode);


    }
}
