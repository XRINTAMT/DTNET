using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models {
    public class Task
    {
        private string taskName;
        private bool isDone = false;
        private int orderNumber;
        private int expectedOrderNumber;

        public Task(string taskName, bool isDone, int orderNumber, int expectedOrderNumber) {
            this.isDone = true;
            this.orderNumber = orderNumber;
            this.expectedOrderNumber = expectedOrderNumber;
        }

        public bool IsDone() {
            return isDone;
        }
    }
}
