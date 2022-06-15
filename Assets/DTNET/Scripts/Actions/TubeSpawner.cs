using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTNET.Actions {
    public class TubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject tubePrefab;

        public void spawnTubeAtPosition() 
        {
            float y_offset = 0.45f;
            Vector3 spawnPosition = transform.position + new Vector3(0, y_offset, 0);
            GameObject tube = Instantiate(tubePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
