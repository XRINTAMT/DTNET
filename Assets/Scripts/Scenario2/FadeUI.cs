using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<CanvasGroup> canvasGroups;
    public bool fadeIn,fadeOut;

    void Start()
    {
        
    }

    public void FadeIn() 
    {
        fadeIn = true;
    }
    public void FadeOut()
    {
        fadeOut = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            fadeOut = false;
            for (int i = 0; i < canvasGroups.Count; i++)
            {
                if (canvasGroups[i].alpha > 0)
                {
                    canvasGroups[i].alpha -= 1f * Time.deltaTime;
                    canvasGroups[i].GetComponent<GraphicRaycaster>().enabled = false;
                }
            }
            if (canvasGroups[canvasGroups.Count-1].alpha<=0)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            fadeIn = false;
            for (int i = 0; i < canvasGroups.Count; i++)
            {
                if (canvasGroups[i].alpha < 1)
                {
                    canvasGroups[i].alpha += 1f *Time.deltaTime;
                    canvasGroups[i].GetComponent<GraphicRaycaster>().enabled = true;
                }
            }
            if (canvasGroups[canvasGroups.Count - 1].alpha >= 1)
            {
                fadeOut = false;
            }
        }
    }
}
