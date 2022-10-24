using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    GameObject ObjSpawn;
    [SerializeField] private Camera cam;
    
    void Start()
    {
        
    }

    public void ChooseObject(string obj) 
    {
        ObjSpawn = Resources.Load("Prefabs/ConstructorEditor/" +obj) as GameObject;
        Debug.Log("Prefabs/ConstructorEditor/" + obj);
        Debug.Log(ObjSpawn);
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && ObjSpawn!=null)
	    {
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
          
            if (Physics.Raycast(ray,out hit) && hit.transform.tag == "Ground") Instantiate(ObjSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            
	    } 
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    } 
}
