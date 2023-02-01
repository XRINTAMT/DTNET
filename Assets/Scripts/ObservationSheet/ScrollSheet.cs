using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSheet : MonoBehaviour
{
    [SerializeField] Scrollbar Scroll;

    public void Refresh()
    {
        float height = GetComponent<RectTransform>().rect.height;
        float y = Mathf.Lerp(-height / 2, - height / 6.11f, Scroll.value);
        //GetComponent<RectTransform>().rect.Set(0, y, GetComponent<RectTransform>().rect.width, height);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().position.x, y);
    }

    public void ScrollButton(float amount)
    {
        Scroll.value += amount;
        if (Scroll.value > 1)
            Scroll.value = 1;
        if (Scroll.value < 0)
            Scroll.value = 0;
        Refresh();
    }
}
