using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models {
    public class TubeSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject _tubePrefab;

        // Update is called once per frame
        void Update()
        {
            
        }


        public void spawnTubeAtPosition() 
        {
            float y_offset = 0.45f;
            Vector3 spawnPosition = transform.position + new Vector3(0, y_offset, 0);
            GameObject tube = Instantiate(_tubePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
