using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Actions {
    public class TubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject tubePrefab;

        private const int MAX_NUM_OF_TUBES = 5;
        private int currentNumberOfTubes = 0;

        public void spawnTubeAtPosition() 
        {
            if(currentNumberOfTubes < MAX_NUM_OF_TUBES) {
                float y_offset = 0.45f;
                Vector3 spawnPosition = transform.position + new Vector3(0, y_offset, 0);
                GameObject tube = Instantiate(tubePrefab, spawnPosition, Quaternion.identity);
                currentNumberOfTubes++;
            }
        }
    }
}
