using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    [SerializeField] private List <GameObject> guidePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GuidePanelActivate(int numberTask) 
    {
        Debug.Log(44);
        bool disactivateAll = false;

        for (int i = 0; i < guidePanel.Count; i++)
        {
            guidePanel[i].SetActive(false);
           
        }
        disactivateAll = true;

        if (disactivateAll)
        {
            guidePanel[numberTask].SetActive(true);
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
