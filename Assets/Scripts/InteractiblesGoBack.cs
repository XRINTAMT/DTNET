using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblesGoBack : MonoBehaviour
{
    [SerializeField] float TemporalOffset = 2;
    [SerializeField] float TimeToGoBack = 1;
    Vector3 initPos;
    bool kinematic;
    Quaternion initAngle;
    Coroutine goingBack;
    [SerializeField] Transform Interactible;

    void Start()
    {
        if (Interactible == null)
        {
            Interactible = transform;
        }
        initPos = Interactible.position;
        initAngle = Interactible.rotation;
        goingBack = null;
        
        if (Interactible.TryGetComponent<Rigidbody>(out _))
        {
            kinematic = Interactible.GetComponent<Rigidbody>().isKinematic;
        }
        
    }

    public void GoBack()
    {
        if (goingBack == null)
        {
            goingBack = StartCoroutine(GoToInitPosition());
        }
    }

    public void UndoGoingBack()
    {
        if(goingBack != null)
        {
            StopCoroutine(goingBack);
            goingBack = null;
        }
    }

    IEnumerator GoToInitPosition()
    {
        for(float i = 0; i < 1; i += Time.deltaTime / TemporalOffset)
        {
            yield return 0;
        }
        Vector3 startingPos = Interactible.position;
        Quaternion startingAngle = Interactible.rotation;
        if(Interactible.TryGetComponent<Rigidbody>(out _))
        {
            Interactible.GetComponent<Rigidbody>().isKinematic = true;
        }
        for (float i = 0; i < 1; i += Time.deltaTime / TimeToGoBack)
        {
            Interactible.rotation = Quaternion.Lerp(startingAngle, initAngle, i);
            Interactible.position = Vector3.Lerp(startingPos, initPos, i);
            yield return 0;
        }
        Interactible.rotation = initAngle;
        Interactible.position = initPos;
        goingBack = null;
        if (Interactible.TryGetComponent<Rigidbody>(out _))
        {
            Interactible.GetComponent<Rigidbody>().isKinematic = kinematic;
        }
    }
}
