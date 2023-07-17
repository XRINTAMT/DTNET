using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirationDate : MonoBehaviour
{
    [SerializeField] private System.DateTime ExpDate = System.DateTime.FromBinary(0);
    [SerializeField] private UnityEngine.UI.Text DateStamp;
    [SerializeField] private Expirable ExpiredVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(int _iter)
    {
        float _threshold = Mathf.Pow(0.5f, _iter);
        if (Random.value < _threshold)
        {
            ExpDate = System.DateTime.Now.AddDays(Random.Range(-5, -1));
            ExpiredVal.Expired = true;
        }
        else
        {
            ExpDate = System.DateTime.Now.AddDays(Random.Range(1, 365));
            ExpiredVal.Expired = false;
        }
        DateStamp.text = ExpDate.Day.ToString("00") + "." + ExpDate.Month.ToString("00") + "." + ExpDate.Year;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
