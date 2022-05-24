using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models.Patient
{
    [System.Serializable]
    public class Referral
    {
        public int id;

        public enum SampleTubes
        {
            RED,
            LIGH_GREEN,
            PURPULE
        }
        public List<SampleTubes> tubesToTake;
    }
}

