using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct BulletListItemLink
{
    public string Description;
    [field: SerializeField] public int ID { get; private set; }
    public BulletListItemLink(int id, Text text, Image tick, BulletListItemLink[] content)
    {
        ID = id;
        Text = text;
        Tick = tick;
        Content = content;
        Description = text.text;
    }
    public BulletListItemLink(string desc = "Error! Element not found!")
    {
        ID = -1;
        Text = null;
        Tick = null;
        Content = null;
        Description = desc;
    }
    public Text Text;
    Image Tick;
    public BulletListItemLink[] Content;
    public BulletListItemLink RecursiveSearch(int id)
    {
        if(ID == id)
        {
            return this;
        }
        for (int i = 0; i < Content.Length; i++)
        {
            BulletListItemLink finding = Content[i].RecursiveSearch(id);
            if (finding.ID != -1)
            {
                return finding;
            }
        }
        BulletListItemLink empty = new BulletListItemLink();
        empty.ID = -1;
        empty.Description = "Error! Element not found!";
        return empty;
    }

    public void Cross( Color color)
    {
        if (ID == -1)
        {
            Debug.LogError("Element not found! You are trying to cross a non-existent element!");
        }
        else
        {
            if (Text == null)
            {
                Debug.Log("text is NULL at " + Description);
            }
            //Text.color = new Color(0.85f, 0.85f, 0.85f);
            Text.color = color;
            Text.text = StrikeThrough(Text.text);
            /*
            GameObject temp = UnityEngine.Object.Instantiate(Text.gameObject);
            //temp.AddComponent<Image>();
            temp.transform.SetParent(Text.transform, false);
            temp.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            temp.GetComponent<RectTransform>().position = new Vector2(0, 0);
            */
            //temp.GetComponent<RectTransform>().rect = Text.flexibleWidth;
        }
    }
    string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }
}

public class TabletBulletList : MonoBehaviour
{
    [SerializeField] BulletListItemLink[] ListItems;
    [SerializeField] float totalHeight;
    [SerializeField] float maskedHeight;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Color colorCross;
    [SerializeField] AudioSource TaskSFX;
    [SerializeField] AudioSource SubtaskSFX;
    public Action<int> crossOut;
    public void Init(BulletListItemLink[] listItems, float theight, float mheight)
    {
        ListItems = listItems;
        totalHeight = theight;
        maskedHeight = mheight;
    }

    public void CrossOut(int _ID)
    {
        FindByID(_ID).Cross(colorCross);
        bool _isSub = true;
        int _topID = 0;

        if (!PhotonManager._viewerApp)
            crossOut?.Invoke(_ID);

        for(int i = 0; i < ListItems.Length; i++)
        {
            if(ListItems[i].ID == _ID)
            {
                _isSub = false;
                _topID = i;
                break;
            }
        }
        if (_isSub)
        {
            if (!TaskSFX.isPlaying)
                SubtaskSFX.Play();
        }
        else
        {
            TaskSFX.Play();
            if (SubtaskSFX.isPlaying)
                SubtaskSFX.Stop();
            if (_topID < ListItems.Length - 1)
            {
                _topID += 1;
                float _scrollAmount = -ListItems[_topID].Text.rectTransform.anchoredPosition.y - 1.5f * ListItems[_topID].Text.rectTransform.sizeDelta.y;
                Debug.Log(_scrollAmount);
                scrollbar.value = Mathf.Max(0, Mathf.Min(1, _scrollAmount / (totalHeight - maskedHeight)));
                Scroll();
                //transform.GetChild(0).transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, _offset);
            }
        }
            


    }

    private BulletListItemLink FindByID(int id)
    {
        for (int i = 0; i < ListItems.Length; i++)
        {
            BulletListItemLink finding;
            finding = ListItems[i].RecursiveSearch(id);
            if(finding.ID != -1)
            {
                return finding;
            }
        }
        return new BulletListItemLink();
    }

    public void Scroll()
    {
        float val = scrollbar.value;
        transform.GetChild(0).transform.GetComponent<RectTransform>().offsetMax = new Vector2(0, val * (totalHeight - maskedHeight));
    }
}
