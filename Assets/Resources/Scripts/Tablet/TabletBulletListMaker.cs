using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TabletBulletListMaker : MonoBehaviour
{
    [SerializeField] GameObject TabletListPrefab;
    [SerializeField] Canvas TabletCanvas;
    [Serializable]
    public struct TextSettingsStorage
    {
        public int FontSize;
        public float LineSpacing;
        public Font Font;
        public Color FontColor;
        public float OffsetX;
        public float OffsetY;
        public float PointIndentation;
    }
    public TextSettingsStorage TextSettings;
    [SerializeField] Sprite TickImage; 
    [Serializable]
    public struct BulletListItem
    {
        public string Text;
        public BulletListItem[] ContainsItems;
    }

    [SerializeField] BulletListItem[] ListItems;
    GameObject TabletList = null;

    public void FormList()
    {
        if(TabletList != null)
        {
            int BulletsToDelete = TabletList.transform.childCount;
            for(int i = BulletsToDelete - 1; i >= 0; i--)
            {
                DestroyImmediate(TabletList.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            if(TabletCanvas.transform.childCount > 0)
            {
                TabletList = TabletCanvas.transform.GetChild(0).gameObject;
            }
            else
            {
                TabletList = Instantiate(TabletListPrefab);
                TabletList.transform.SetParent(TabletCanvas.transform, false);
                TabletList.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                TabletList.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                TabletList.GetComponent<RectTransform>().anchorMax = new Vector2(0, 2);
            }
        }
        float x = TextSettings.OffsetX;
        float y = -TextSettings.OffsetY;
        int id = 0;
        BulletListItemLink[] Links = new BulletListItemLink[ListItems.Length];
        for(int i = 0; i < ListItems.Length; i++)
        {
            Links[i] = RecursiveBulletListFormation(ListItems[i], x, ref y, ref id);
        }
        TabletList.GetComponent<TabletBulletList>().Init(Links);
    }

    public BulletListItemLink RecursiveBulletListFormation(BulletListItem Item, float x, ref float y, ref int id)
    {
        BulletListItemLink[] contents = new BulletListItemLink[Item.ContainsItems.Length];
        Text text = addUIText(x, y, Item.Text);
        y -= TextSettings.LineSpacing + TextSettings.FontSize;
        int ID = id;
        id++;
        for (int i = 0; i < Item.ContainsItems.Length; i++)
        {
            contents[i] = RecursiveBulletListFormation(Item.ContainsItems[i], x + TextSettings.PointIndentation, ref y, ref id);
        }
        return new BulletListItemLink(ID, text, null, contents);
    }

    private Text addUIText(float x, float y, string text)
    {
        GameObject textObj = new GameObject(text,typeof(RectTransform));
        textObj.transform.SetParent(TabletList.transform, false);
        Text caption = textObj.AddComponent<Text>();
        caption.text = text;
        caption.color = TextSettings.FontColor;
        caption.font = TextSettings.Font;
        caption.fontSize = TextSettings.FontSize;
        textObj.GetComponent<RectTransform>().offsetMin = new Vector2(x, y - TextSettings.LineSpacing - TextSettings.FontSize);
        textObj.GetComponent<RectTransform>().offsetMax = new Vector2(TabletCanvas.GetComponent<RectTransform>().offsetMax.x*2 - TextSettings.OffsetX, y);
        return caption;
    }
}
