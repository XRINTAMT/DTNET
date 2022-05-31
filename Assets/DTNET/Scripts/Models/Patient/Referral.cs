using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models.Patient
{
    [System.Serializable]
    public class Referral
    {
        public int id;


        /*[System.Serializable]
        public class SampleTube {
            public enum TubeColor
            {
                RED,
                LIGH_GREEN,
                PURPULE
            }
            public TubeColor color;
            public string analysis;
        }*/

        //public Dictionary<TubeColor, string> blaaa;
        public List<SampleTube> tubesToTake;
        //public List<string> tubesToTake;
        public bool shouldBeFasting;
    }
}

