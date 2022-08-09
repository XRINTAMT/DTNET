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
        else
        {
            for (int i = 0; i < Content.Length; i++)
            {
                BulletListItemLink finding = Content[i].RecursiveSearch(id);
                if (finding.ID != -1)
                {
                    return finding;
                }
            }
        }
        return new BulletListItemLink();
    }

    public void Cross()
    {
        if (ID == -1)
        {
            Debug.LogError("Element not found! You are trying to cross a non-existent element!");
        }
        else
        {
            Text.color = new Color(0.15f,1f,0.74f);
        }
    }
}

public class TabletBulletList : MonoBehaviour
{
    [SerializeField] BulletListItemLink[] ListItems;
    [SerializeField] float totalHeight;
    [SerializeField] float maskedHeight;
    [SerializeField] Scrollbar scrollbar;

    public void Init(BulletListItemLink[] listItems, float theight, float mheight)
    {
        ListItems = listItems;
        totalHeight = theight;
        maskedHeight = mheight;
    }

    public void CrossOut(int ID)
    {
        FindByID(ID).Cross();
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
