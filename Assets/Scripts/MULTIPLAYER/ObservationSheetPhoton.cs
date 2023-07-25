using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ObservationSheetPhoton : MonoBehaviour
{
    [SerializeField] FadeUI fadeUI;
    [SerializeField] MultipleChoiceRow [] multipleChoiceRow;
    [SerializeField] MultipleChoiceRow multipleChoiceRow1;
    bool add;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

       
      
    }
    // Start is called before the first frame update
    void Start()
    {
        SheetController[] sheetController = FindObjectsOfType<SheetController>();
        multipleChoiceRow = FindObjectsOfType<MultipleChoiceRow>(true);

        for (int i = 0; i < multipleChoiceRow.Length; i++)
        {
            if (!PhotonManager._viewerApp)
            {
                multipleChoiceRow[i].submit += Submit;
            }
        }
      


        for (int i = 0; i < sheetController.Length; i++)
        {
            if (sheetController[i].GetComponent<FadeUI>())
            {
                fadeUI = sheetController[i].GetComponent<FadeUI>();

                if (!PhotonManager._viewerApp)
                {
                    fadeUI._fadeIn += FadeIn;
                    fadeUI._fadeOut += FadeOut;
                }
            }
        }
    }


    void Submit(MultipleChoiceRow multipleChoice) 
    {
        if (!PhotonManager._viewerApp)
        {
            for (int i = 0; i < multipleChoiceRow.Length; i++)
            {
                if (multipleChoice= multipleChoiceRow[i])
                {
                    GetComponent<PhotonView>().RPC("SubmitRPC", RpcTarget.All,i);
                }
            }
        }

    }


    [PunRPC]
    void SubmitRPC(int count)
    {
        Debug.Log("SubmitRPC");

        if (PhotonManager._viewerApp)
        {
            multipleChoiceRow[count].Submit();
        }
    }
    void FadeIn() 
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("FadeInRPC", RpcTarget.All);
        }
    
    }

    void FadeOut()
    {
        if (!PhotonManager._viewerApp)
        {
            GetComponent<PhotonView>().RPC("FadeOutRPC", RpcTarget.All);
        }
    }


    [PunRPC]
    void FadeInRPC()
    {
        Debug.Log("FadeInRPC");
        if (PhotonManager._viewerApp)
        {
            fadeUI.FadeIn();
        }
    }
    [PunRPC]
    void FadeOutRPC()
    {
        Debug.Log("FadeOutRPC");
        if (PhotonManager._viewerApp)
        {
            fadeUI.FadeOut();
        }
      
    }
    // Update is called once per frame
 
}
