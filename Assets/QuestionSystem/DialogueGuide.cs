using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGuide : MonoBehaviour
{
    [SerializeField] GameObject[] GuidePanels;
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GuidePanels[0].SetActive(true);
    }

    public void SetState(int stateID)
    {
        if(Mathf.Abs(stateID - currentIndex) != 1)
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
