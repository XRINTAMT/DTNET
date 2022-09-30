using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacklistedVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InteractiblesGoBack goback;
        if (other.TryGetComponent<InteractiblesGoBack>(out goback))
        {
            goback.GoBack();
        }
    }
}
