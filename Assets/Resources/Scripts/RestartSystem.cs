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
        if(SavedState != null){
            Object.Destroy(SavedState);
        }
        SavedState = Object.Instantiate(Changables);
        SavedState.SetActive(false);
    }

    public void Load(string text = ""){
        if(SavedState == null){
            return;
        }
        Object.Destroy(Changables);
        Changables = Object.Instantiate(SavedState);
        Changables.SetActive(true);
        FindObjectOfType<FadeMessageManager>().FadeWithText(text);
    }

    void Update()
    {
        
    }
}
