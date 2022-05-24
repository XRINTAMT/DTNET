using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Models.Patient
{
    [System.Serializable]
    public class Patient
    {
        #region attributes
        public int id;
        public string fullName;

        public Referral referral;
        #endregion
    }
}
