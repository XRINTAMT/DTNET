using UnityEngine;
using Autohand;

[RequireComponent(typeof(PlacePoint))]
public class TrashBin : MonoBehaviour
{
    [HideInInspector] private PlacePoint placePoint;
    [SerializeField] private MeshRenderer highlight;

    private void Start()
    {
        placePoint = GetComponent<PlacePoint>();
        placePoint.OnPlaceEvent += OnPlace;
    }

    public void OnPlace(PlacePoint point, Grabbable grab)
    {
        highlight.enabled = false;
        Invoke("ReenableHighlight", 0.3f);
        //placePoint.OnStopHighlight.Invoke(placePoint, grab); //invoking a normal PlacePoint method did not work for whatever reason
        Debug.Log("destroyed the " + grab.gameObject.name);
        grab.gameObject.SetActive(false);
        //Destroy(grab.gameObject);
    }
    
    private void ReenableHighlight()
    {
        highlight.enabled = true;
        highlight.gameObject.SetActive(false);
    }
}