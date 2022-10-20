using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<GameObject> obj;
    [SerializeField] SpawnObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChooseObj(int numberChoose) 
    {
        spawnObject.ChooseObject(obj[numberChoose]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
