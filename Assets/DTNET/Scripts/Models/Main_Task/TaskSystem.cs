using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models {

    public class TaskSystem : MonoBehaviour
    {
        private int NumberOfTasks = 5;
        private int currentTaskOrder = 1;

        private Dictionary<string, int> _tasksDone = new Dictionary<string, int>();

        void Update() {
            if(allTasksIsCompleted()) {
                Debug.Log("All tasks Completed!");
            }
        }

        public void SinkUsed() {
            _tasksDone.Add("Sink", currentTaskOrder);
            Debug.Log("Sink Used!, Order:: "+currentTaskOrder);
            currentTaskOrder++;
        }

        public void PutOnGloves() {
            _tasksDone.Add("Gloves", currentTaskOrder);
            Debug.Log("Put On Gloves, Order:: "+currentTaskOrder);
            currentTaskOrder++;
        }

        public void askedForIDDone() 
        {
            _tasksDone.Add("AskedId", currentTaskOrder);
            Debug.Log("asked for ID!, Order:: "+currentTaskOrder);
            currentTaskOrder++;
        }

        public void checkedReferralDone()
        {
            _tasksDone.Add("referral", currentTaskOrder);
            Debug.Log("checkedReferralDone, Order:: "+currentTaskOrder);
            currentTaskOrder++;
        }

        public void hasCollectedAllMedicalMaterials() {
            _tasksDone.Add("Materials", currentTaskOrder);
            Debug.Log("hasCollectedAllMedicalMaterials, Order:: "+currentTaskOrder);
            currentTaskOrder++;
        }

        public bool allTasksIsCompleted() {
            //return (hasAskedForID && hasOpenedReferral && hasUsedTheSink && hasGlovesOn);
            return (currentTaskOrder > NumberOfTasks);
        }
    }
}
