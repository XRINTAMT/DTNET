using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DTNET.Handlers {
    public class TaskTrackingHandler : MonoBehaviour
    {
        public AllTaskCompletedUIHandler tasksCompletedCanvas;

        private int NumberOfTasks = 5;
        private int currentTaskOrder = 1;
        private string hygienKey = "Basic hygien";
        private string askIdKey = "Asked For ID";
        private string referralMatchKey = "Match With Referral";
        private string materialsKey = "Collect All Materials";
        private string glovesKey = "Put On Gloves";

        private bool allTaskCompleted = false;

        private Dictionary<string, int> correctOrder;
        private Dictionary<string, int> completedTasks = new Dictionary<string, int>();

        // Start is called before the first frame update
        void Start()
        {
            correctOrder = new Dictionary<string, int>(NumberOfTasks);
            correctOrder.Add(hygienKey, 1);
            correctOrder.Add(askIdKey, 2);
            correctOrder.Add(referralMatchKey, 3);
            correctOrder.Add(materialsKey, 4);
            correctOrder.Add(glovesKey, 5);
        }

        // Update is called once per frame
        void Update()
        {
            if(!allTasksIsCompleted()) return;

            if(allTaskCompleted) return;
            allTaskCompleted = true;
            Debug.Log("All tasks Completed!");
 
            tasksCompletedCanvas.Show(GetResults());
            //cameraUI.DisplayMessage("Congratulations\nYou have completed all tasks");
        }

        public void HygienTaskCompleted() 
        {
            addToCompletedTasks(hygienKey);
        }

        public void AskForPatientIDTaskCompleted() 
        {
            addToCompletedTasks(askIdKey);
        }

        public void CheckReferralTaskCompleted() 
        {
            addToCompletedTasks(referralMatchKey);
        }

        public void CollectedAllMaterialsTaskCompleted()
        {
            addToCompletedTasks(materialsKey);
        }

        public void PutOnGlovesTaskCompleted()
        {
            addToCompletedTasks(glovesKey);
        }


    // ==================================================================================
        private void addToCompletedTasks(string key) {
            try {
                completedTasks.Add(key, currentTaskOrder);
                Debug.Log("Task: '"+key+"' Done! Order:: "+currentTaskOrder);
                currentTaskOrder++;
            } catch (System.Exception e)  
            {  
                Debug.Log("Task already done: "+key);
                //Debug.Log(e.Message);
            }
        }

        public string GetResults() {
            string results = "You completed all tasks for a Venous Blood Sampling Preperation.\n";
            results += "Results:\n";
            results += TaskResultStringFromat(hygienKey);
            results += TaskResultStringFromat(askIdKey);
            results += TaskResultStringFromat(referralMatchKey);
            results += TaskResultStringFromat(materialsKey);
            results += TaskResultStringFromat(glovesKey);
            return results;
        }

        private string TaskResultStringFromat(string key) {
            return (key + " Your order:  " + completedTasks[key] + ", Should be: "+correctOrder[key] + "\n");
        }

        public bool allTasksIsCompleted() {
            return (currentTaskOrder > NumberOfTasks);
        }
    }
}
