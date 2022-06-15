using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models
{
    [System.Serializable]
    public class Referral
    {
        public int id;
        public List<SampleTube> tubesToTake;
        public bool shouldBeFasting;
    }
}
