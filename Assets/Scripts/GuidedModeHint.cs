using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedModeHint : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("GuidedMode") != 1)
        {
            gameObject.SetActive(false);
        }
    }
}
