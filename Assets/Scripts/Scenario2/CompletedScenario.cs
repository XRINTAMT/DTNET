using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedScenario : MonoBehaviour
{
    [SerializeField] GameObject panelCompletedScenario;
    [SerializeField] Image moodStatus;
    [SerializeField] List<Sprite> moodList;
    [SerializeField] Text answersProgress;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetData(int indexMoodList, int totlaInformation, int totlaFound) 
    {
        panelCompletedScenario.SetActive(true);
        moodStatus.sprite = moodList[indexMoodList];
        answersProgress.text = "" + totlaFound  + "/" + totlaInformation /*+ "%"*/;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
