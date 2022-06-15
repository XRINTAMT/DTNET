using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.Handlers;

namespace DTNET.Tasks {
    public class PutOnGlovesTask : MonoBehaviour
    {
        public TaskTrackingHandler taskTracking;
        public CameraUIHandler cameraUI;

        public void PerformPutOnGlovesTask()
        {
            taskTracking.PutOnGlovesTaskCompleted();
            cameraUI.DisplayMessage("This is the final task\nYou should now be ready perform a blood sample");
        }
    }
}

