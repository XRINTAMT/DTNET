using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientController : MonoBehaviour
{
    [SerializeField] List<GameObject> patient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPatient(int indexPatient) 
    {
        switch (indexPatient)
        {
            case 1:
                patient[0].SetActive(true);
                break;
            case 2:
                patient[1].SetActive(true);
                break;
            case 3:
                patient[2].SetActive(true);
                break;
            case 4:
                patient[3].SetActive(true);
                break;
            default:
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
