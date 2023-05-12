using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSystem : MonoBehaviour
{
    [SerializeField] GameObject Changables;
    GameObject SavedState;


    void Start()
    {
        Save();
    }

    public void Save(){
        if(PlayerPrefs.GetInt("GuidedMode", 1) == 1)
        {
            Invoke("MakeASave", 1);
        }
    }

    private void MakeASave()
    {
        if (SavedState != null)
        {
            //Object.Destroy(SavedState);
        }
        
        SavedState = Object.Instantiate(Changables);
        SavedState.SetActive(false);
        SavedState.GetComponentInChildren<OldScenarioBehaviour>().Activate(false);
    }

    public void Load(string text = "") {

        if (PlayerPrefs.GetInt("GuidedMode") == 1) text = "That is not what you should be doing right now. Check the tasks list!";

        if (SavedState == null) {
            return;
        }
        StartCoroutine(LoadDelay());
        foreach(FadeMessageManager Fade in FindObjectsOfType<FadeMessageManager>())
        {
            Fade.FadeWithText(text);
        }
    }

    IEnumerator LoadDelay()
    {
        for(float i = 0; i < 2; i += Time.deltaTime)
        {
            yield return 0;
        }
        Object.Destroy(Changables);
        Changables = Object.Instantiate(SavedState);
        Changables.SetActive(true);
        Changables.GetComponentInChildren<OldScenarioBehaviour>().Activate(true);
    }

    void Update()
    {
        
    }
}
