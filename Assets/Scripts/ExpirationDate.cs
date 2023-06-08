using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirationDate : MonoBehaviour
{
    [SerializeField] private System.DateTime ExpDate = System.DateTime.FromBinary(0);
    [SerializeField] private UnityEngine.UI.Text DateStamp;
    // Start is called before the first frame update
    void Start()
    {
        if (ExpDate.CompareTo(System.DateTime.FromBinary(0)) == 0)
        {
            ExpDate = System.DateTime.Now.AddDays(Random.Range(-182, 365));
            DateStamp.text = ExpDate.Day.ToString("00") + "." + ExpDate.Month.ToString("00") + "." + ExpDate.Year;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
