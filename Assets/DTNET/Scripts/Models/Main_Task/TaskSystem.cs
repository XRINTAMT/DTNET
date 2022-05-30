using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.UI;

namespace DTNET.Models {

    public class TaskSystem : MonoBehaviour
    {

        public AllTasksCompleted allTasksCompletedCanvas;
        private int NumberOfTasks = 5;
        private int currentTaskOrder = 1;

        private string hyginKey = "Basic Hygin";
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
            correctOrder.Add(materialsKey, 2);
            correctOrder.Add(askIdKey, 3);
            correctOrder.Add(referralMatchKey, 4);
            correctOrder.Add(glovesKey, 5);
        }

        void Update() {
            if(allTasksIsCompleted()) {
                Debug.Log("All tasks Completed!");
                if(hasNotCompleted) 
                {
                    allTasksCompletedCanvas.Show(GetResults());
                    hasNotCompleted = false;
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
            string results = "Tasks Results:\n";
            results += (hyginKey + "Task Order: " + _tasksDone[hyginKey] + ", Should be: "+correctOrder[hyginKey]) + "\n";
            results += (materialsKey + "Task Order: " + _tasksDone[materialsKey] + ", Should be: "+correctOrder[materialsKey])+ "\n";
            results += (askIdKey + "Task Order: " + _tasksDone[askIdKey] + ", Should be: "+correctOrder[askIdKey])+ "\n";
            results += (referralMatchKey + "Task Order: " + _tasksDone[referralMatchKey] + ", Should be: "+correctOrder[referralMatchKey])+ "\n";
            results += (glovesKey + "Task Order: " + _tasksDone[glovesKey] + ", Should be: "+correctOrder[glovesKey])+ "\n";
            return results;
        }

        public bool allTasksIsCompleted() {
            return (currentTaskOrder > NumberOfTasks);
        }
    }
}
