using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class FadeMessageManager : MonoBehaviour
{
    [SerializeField] Image fadeBackground;
    [SerializeField] Text displayText;
    [SerializeField] float fadeTime;
    [SerializeField] UnityEvent OnStartFade;
    [SerializeField] UnityEvent OnEndFade;
    public Action<string> fadeStart;

    void Start()
    {
     
    }

    private void Awake()
    {
        fadeBackground.color = alterAlpha(fadeBackground.color, 0);
        displayText.color = alterAlpha(displayText.color, 0);
    }

    // Update is called once per frame


    public void FadeWithText(string text)
    {
        displayText.text = text;
        fadeStart?.Invoke(text);
        StartCoroutine(fadeAnimation());
    }

    public void FadeWallTrue()
    {
        StartCoroutine(fadeAnimationWallIn());
        OnStartFade.Invoke();
    }
    public void FadeWallFalse()
    {
        StartCoroutine(fadeAnimationWallOut());
    }

    IEnumerator fadeAnimation()
    {
        float episodeTime = fadeTime / 3;
        OnStartFade.Invoke();
        for (float i = 0; i < 1; i += Time.deltaTime / episodeTime)
        {
            fadeBackground.color = alterAlpha(fadeBackground.color, i);
            displayText.color = alterAlpha(displayText.color, i);
            yield return 0;
        }
        fadeBackground.color = alterAlpha(fadeBackground.color, 1);
        displayText.color = alterAlpha(displayText.color, 1);
        for (float i = 0; i < 1; i += Time.deltaTime / episodeTime)
            yield return 0;
        for (float i = 0; i < 1; i += Time.deltaTime / episodeTime)
        {
            fadeBackground.color = alterAlpha(fadeBackground.color, 1 - i);
            displayText.color = alterAlpha(displayText.color, 1 - i);
            yield return 0;
        }
        fadeBackground.color = alterAlpha(fadeBackground.color, 0);
        displayText.color = alterAlpha(displayText.color, 0);
        OnEndFade.Invoke();
    }

    IEnumerator fadeAnimationWallIn()
    {
        float episodeTime = fadeTime / 10;
        for (float i = 0; i < 1; i += Time.deltaTime / episodeTime)
        {
            fadeBackground.color = alterAlpha(fadeBackground.color, i);
            yield return 0;
        }
        fadeBackground.color = alterAlpha(fadeBackground.color, 1);
    }
    IEnumerator fadeAnimationWallOut()
    {
        float episodeTime = fadeTime / 10;
        for (float i = 0; i < fadeBackground.color.a; i += Time.deltaTime / episodeTime)
        {
            fadeBackground.color = alterAlpha(fadeBackground.color, fadeBackground.color.a- i);
            yield return 0;
        }
        fadeBackground.color = alterAlpha(fadeBackground.color, 0);
        OnEndFade.Invoke();
    }
    Color alterAlpha(Color c, float alpha)
    {
        Color tempcolor = c;
        tempcolor.a = alpha;
        return tempcolor;
    }


}
