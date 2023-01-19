using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientController : MonoBehaviour
{
    [System.Serializable]
    class PatientDialoguePair
    {
        public GameObject patient;
        public GameObject dialogue;
        public GameObject offset;
    }
    [SerializeField] List<PatientDialoguePair> patient;

    // Start is called before the first frame update
    void Start()
    {
        int id = Random.Range(0, patient.Count);
        patient[id].patient.SetActive(true);
        patient[id].dialogue.SetActive(true);
        patient[id].patient.GetComponent<HeadCharacterFollow>().player=patient[id].offset.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
