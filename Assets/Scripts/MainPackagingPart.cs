using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPackagingPart : MonoBehaviour
{
    [SerializeField] Packaging packaging;

    /*
    private void OnDestroy()
    {
        packaging.MainPackagingDestroyed();
    }
    */

    private void OnDisable()
    {
        packaging.MainPackagingDisabled();
    }
}
