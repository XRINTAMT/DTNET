using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class FPSAnalize : MonoBehaviour
{
    Renderer[] arrayRend;
    Grabbable[] arrayGrab;
    Wire[] arrayWire;
    Canvas[] arrayCanvas;
    public bool wiresToggle;
    public bool grabToggle;
    public bool rendToggle;
    public bool canvasToggle;
    public Canvas fps;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToogleCanvas()
    {
        arrayCanvas = FindObjectsOfType<Canvas>();

        for (int i = 0; i < arrayCanvas.Length; i++)
        {
            arrayCanvas[i].enabled = !arrayCanvas[i].enabled;
        }
        fps.enabled = true;
    }
    public void ToogleGrabbeble()
    {
        arrayGrab = FindObjectsOfType<Grabbable>();

        for (int i = 0; i < arrayGrab.Length; i++)
        {
            arrayGrab[i].enabled= !arrayGrab[i].enabled;
        }
    }
    public void ToggleRenderer()
    {
        arrayRend = FindObjectsOfType<Renderer>();

        for (int i = 0; i < arrayRend.Length; i++)
        {
            arrayRend[i].enabled = !arrayRend[i].enabled;
        }
    }

    public void ToggleWires()
    {
        arrayWire = FindObjectsOfType<Wire>();

        for (int i = 0; i < arrayRend.Length; i++)
        {
            arrayWire[i].enabled = !arrayWire[i].enabled;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (grabToggle)
        {
            ToogleGrabbeble();
            grabToggle = false;
        }
        if (rendToggle)
        {
            ToggleRenderer();
            rendToggle = false;
        }
        if (wiresToggle)
        {
            ToggleRenderer();
            wiresToggle = false;
        }
        if (canvasToggle)
        {
            ToogleCanvas();
            canvasToggle = false;
        }
    }
}
