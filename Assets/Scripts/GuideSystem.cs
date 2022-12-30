using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    [SerializeField] private List <GameObject> guidePanel;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject dialogueGuide;
    // Start is called before the first frame update
    void Start()
    {
        if (canvas.activeInHierarchy)
        {
            dialogueGuide.SetActive(true);
        }
    }

    public void GuidePanelActivate(int numberTask) 
    {
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
