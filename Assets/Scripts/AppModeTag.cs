using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppModeTag : MonoBehaviour
{
    [SerializeField] string SetMode;

    void Start()
    {
        PlayerPrefs.SetString("AppMode", SetMode);   
    }
}
