using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    GameObject ObjSpawn;
    [SerializeField] private Camera cam;
    ObjectsOnTheScene Objects;
    
    void Start()
    {
        Objects = GetComponent<ObjectsOnTheScene>();
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

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
            {
                GameObject temp = Instantiate(ObjSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                Objects.Add(temp.GetComponent<PlacedObjectBehaviour>());
                this.enabled = false;
            }
	    } 
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    } 
}
