using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblesGoBack : MonoBehaviour
{
    [SerializeField] float TemporalOffset;
    [SerializeField] float TimeToGoBack;
    Vector3 initPos;
    Quaternion initAngle;
    Coroutine goingBack;

    void Start()
    {
        initPos = transform.position;
        initAngle = transform.rotation;
        goingBack = null;
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
        Vector3 startingPos = transform.position;
        Quaternion startingAngle = transform.rotation;
        if(TryGetComponent<Rigidbody>(out _))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        for (float i = 0; i < 1; i += Time.deltaTime / TimeToGoBack)
        {
            transform.rotation = Quaternion.Lerp(startingAngle, initAngle, i);
            transform.position = Vector3.Lerp(startingPos, initPos, i);
            yield return 0;
        }
        transform.rotation = initAngle;
        transform.position = initPos;
        goingBack = null;
    }
}
