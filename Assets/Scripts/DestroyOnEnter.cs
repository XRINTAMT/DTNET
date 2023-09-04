using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyOnEnter : MonoBehaviour
{
    [SerializeField] private string[] ToDestroyNames;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You just threw out: " + other.gameObject.name);
        if (ToDestroyNames.Contains(other.gameObject.name))
        {
            StartCoroutine(DestroyGameObject(other.gameObject));
        }
    }

    private IEnumerator DestroyGameObject(GameObject _toDestroy)
    {
        Vector3 initScale = _toDestroy.transform.localScale;
        for(float i = 0; i < 1; i += Time.deltaTime)
        {
            _toDestroy.transform.localScale = initScale * (1 - i);
            yield return 0;
        }

        //Destroy(_toDestroy);
        _toDestroy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
