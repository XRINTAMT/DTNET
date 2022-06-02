using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.UI;

namespace DTNET.Models {

    public class TaskSystem : MonoBehaviour
    {

        public AllTasksCompleted allTasksCompletedCanvas;

        public CameraUI cameraUI;
        private int NumberOfTasks = 5;
        private int currentTaskOrder = 1;
        private string hyginKey = "Basic hygien";
        private string materialsKey = "Collect All Materials";
        private string askIdKey = "Ask For ID";
        private string referralMatchKey = "Match With Referral";
        private string glovesKey = "Put On Gloves";

        private bool hasNotCompleted = true;

        private Dictionary<string, int> correctOrder;

        private Dictionary<string, int> _tasksDone = new Dictionary<string, int>();

        void Start() {
            correctOrder = new Dictionary<string, int>(NumberOfTasks);
            correctOrder.Add(hyginKey, 1);
            correctOrder.Add(askIdKey, 2);
            correctOrder.Add(referralMatchKey, 3);
            correctOrder.Add(materialsKey, 4);
            correctOrder.Add(glovesKey, 5);
        }

        void Update() {
            if(allTasksIsCompleted()) {
                Debug.Log("All tasks Completed!");
                if(hasNotCompleted) 
                {
                    allTasksCompletedCanvas.Show(GetResults());
                    hasNotCompleted = false;
                    cameraUI.DisplayMessage("Congratulations\nYou have completed all tasks");
                }
            }
        }

        private void addToTaskDone(string key) {
            try {
                _tasksDone.Add(key, currentTaskOrder);
                Debug.Log("Task: '"+key+"' Done! Order:: "+currentTaskOrder);
                currentTaskOrder++;
            } catch (System.Exception e)  
            {  
                Debug.Log("Task already done: "+key);
                //Debug.Log(e.Message);
            }
        }

        public void SinkUsed() {
            addToTaskDone(hyginKey);
        }
        public void hasCollectedAllMedicalMaterials() {
            addToTaskDone(materialsKey);
        }

        public void askedForIDDone() 
        {
            addToTaskDone(askIdKey);
        }

        public void checkedReferralDone()
        {
            addToTaskDone(referralMatchKey);
        }

        public void PutOnGloves() {
            addToTaskDone(glovesKey);
        }

        public string GetResults() {
            string results = "You completed all tasks for a Venous Blood Sampling Preperation.\n";
            results += "Results:\n";
            results += TaskResultStringFromat(hyginKey);
            results += TaskResultStringFromat(askIdKey);
            results += TaskResultStringFromat(referralMatchKey);
            results += TaskResultStringFromat(materialsKey);
            results += TaskResultStringFromat(glovesKey);
            return results;
        }

        private string TaskResultStringFromat(string key) {
            return (key + " Your order:  " + _tasksDone[key] + ", Should be: "+correctOrder[key] + "\n");
        }

        public bool allTasksIsCompleted() {
            return (currentTaskOrder > NumberOfTasks);
        }
    }
}
