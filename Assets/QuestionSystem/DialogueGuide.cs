using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGuide : MonoBehaviour
{
    [SerializeField] GameObject[] GuidePanels;
    int currentIndex = 0;
    [SerializeField] bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        if(active)
            GuidePanels[0].SetActive(true);
    }

    public void SetState(int stateID)
    {
        if (!active)
        {
            return;
        }
        if (Mathf.Abs(stateID - currentIndex) != 1)
        {
            return;
        }
        GuidePanels[currentIndex].SetActive(false);
        currentIndex = stateID;
        GuidePanels[currentIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
