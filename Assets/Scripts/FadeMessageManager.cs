using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeMessageManager : MonoBehaviour
{
    [SerializeField] Image fadeBackground;
    [SerializeField] Text displayText;
    [SerializeField] float fadeTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeWithText(string text)
    {
        displayText.text = text;
        StartCoroutine(fadeAnimation());
    }

    IEnumerator fadeAnimation()
    {
        float episodeTime = fadeTime / 3;
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
    }

    Color alterAlpha(Color c, float alpha)
    {
        Color tempcolor = c;
        tempcolor.a = alpha;
        return tempcolor;
    }
}
