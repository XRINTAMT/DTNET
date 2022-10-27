using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    GameObject ObjSpawn;
    GameObject ObjSpawnMarker;
    [SerializeField] private Camera cam;
    ObjectsOnTheScene Objects;
    bool instMarker;

    
    void Start()
    {
        if (cam != null) cam = Camera.main;
        
        Objects = GetComponent<ObjectsOnTheScene>();
    }

    public void ChooseObject(string obj) 
    {
        ObjSpawn = Resources.Load("Prefabs/ConstructorEditor/" +obj) as GameObject;
        Debug.Log("Prefabs/ConstructorEditor/" + obj);
        Debug.Log(ObjSpawn);
        instMarker = true;


    }
    public void ChooseObjectPrfab(GameObject obj)
    {
        ObjSpawn = obj;
       
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
        {
            if (ObjSpawn!=null && instMarker)
            {
                ObjSpawnMarker = Instantiate(ObjSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                ObjSpawnMarker.GetComponent<Collider>().enabled = false;
                instMarker = false;
            }
            if (ObjSpawnMarker!=null)
            {
                ObjSpawnMarker.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
        if (Input.GetMouseButtonDown(0) && ObjSpawn!=null)
	    {
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
            {
                GameObject temp = Instantiate(ObjSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                Objects.Add(temp.GetComponent<PlacedObjectBehaviour>());
                Destroy(ObjSpawnMarker);
                this.enabled = false;
                enabled = false;
            }
	    } 
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    } 
}
