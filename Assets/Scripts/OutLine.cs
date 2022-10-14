using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;
    GameObject outlineObject;

    public bool renderOutline;
    void Start()
    {
        //outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        //outlineRenderer.enabled = true;
    }

    private void Update()
    {
        if (renderOutline)
        {
            outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
            outlineRenderer.enabled = true;
            renderOutline = false;
        }
    }
    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        if(outlineObject!=null) Destroy(outlineObject);

        outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutLine>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
