using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DTNET.Actions;

namespace DTNET.Handlers {
    public class TaskTrackingHandler : MonoBehaviour
    {
        public AllTaskCompletedUIHandler tasksCompletedCanvas;
        [SerializeField] private VisualGuideHandler visualGuide;

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

            Vector3 sinkPos = new Vector3(-2.811f,1.2f, -0.536f);
            visualGuide.setPosition(sinkPos);
        }

        // Update is called once per frame
        void Update()
        {
            if(!allTasksIsCompleted()) return;

            if(allTaskCompleted) return;
            allTaskCompleted = true;
            Debug.Log("All tasks Completed!");
 
            tasksCompletedCanvas.Show(GetResults());
        }

        public void HygienTaskCompleted() 
        {
            addToCompletedTasks(hygienKey);
            Vector3 patientPos = new Vector3(2.122f, 1.013f, -0.723f);
            visualGuide.setPosition(patientPos);
        }

        public void AskForPatientIDTaskCompleted() 
        {
            addToCompletedTasks(askIdKey);
            Vector3 computerPos = new Vector3(-1.87f, 0.85f, 1.6f);
            visualGuide.setPosition(computerPos);
        }

        public void CheckReferralTaskCompleted() 
        {
            addToCompletedTasks(referralMatchKey);
            Vector3 materialPlacementPos = new Vector3(1.34f, 0.892f, -1.497f);
            visualGuide.setPosition(materialPlacementPos);
        }

        public void CollectedAllMaterialsTaskCompleted()
        {
            addToCompletedTasks(materialsKey);
            Vector3 glovesPos = new Vector3(0.896f, 0.852f, -1.5f);
            visualGuide.setPosition(glovesPos);
        }

        public void PutOnGlovesTaskCompleted()
        {
            addToCompletedTasks(glovesKey);
            visualGuide.hide();
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
