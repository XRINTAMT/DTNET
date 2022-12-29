using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButtonBehaviour : MonoBehaviour
{
    [SerializeField] Image[] Icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Refresh(string topic)
    {
        if(topic == null)
        {
            foreach(Image image in Icon){
                image.enabled = false;
            }
            return;
        }
        Sprite currentSprite = Resources.Load<Sprite>("TopicIcons/" + topic);
        foreach (Image image in Icon)
        {
            image.enabled = true;
            image.sprite = currentSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
