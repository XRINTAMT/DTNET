using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : DataSaver
{
    // Start is called before the first frame update
    [SerializeField] List<CanvasGroup> canvasGroups;
    private float[] SavedAlphas;
    [SerializeField] float speedMultiplier = 1;
    public bool fadeIn,fadeOut;
    public Action _fadeIn;
    public Action _fadeOut;
    void Start()
    {
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            GraphicRaycaster _raycaster;
            if(canvasGroups[i].TryGetComponent<GraphicRaycaster>(out _raycaster))
                _raycaster.enabled = false;
        }
        SavedAlphas = new float[canvasGroups.Count];
    }

    public void FadeIn() 
    {
        fadeIn = true;
        _fadeIn?.Invoke();
    }
    public void FadeOut()
    {
        fadeOut = true;
        _fadeOut?.Invoke();
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
                    canvasGroups[i].alpha -= 1f * Time.deltaTime * speedMultiplier;
                    GraphicRaycaster _raycaster;
                    if (canvasGroups[i].TryGetComponent<GraphicRaycaster>(out _raycaster))
                        _raycaster.enabled = false;
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
                    canvasGroups[i].alpha += 1f *Time.deltaTime * speedMultiplier;
                    GraphicRaycaster _raycaster;
                    if (canvasGroups[i].TryGetComponent<GraphicRaycaster>(out _raycaster))
                        _raycaster.enabled = true;
                }
            }
            if (canvasGroups[canvasGroups.Count - 1].alpha >= 1)
            {
                fadeOut = false;
            }
        }
    }

    public override void Save()
    {
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            SavedAlphas[i] = canvasGroups[i].alpha;
        }
    }

    public override void Load()
    {
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            canvasGroups[i].alpha = SavedAlphas[i];
        }
    }
}
