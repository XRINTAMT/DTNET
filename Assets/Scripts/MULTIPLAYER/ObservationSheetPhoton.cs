using System.Collections;
using System.Collections.Generic;
using Autohand;
using Photon.Pun;
using UnityEngine;

public class ObservationSheetPhoton : MonoBehaviour
{
    [SerializeField] FadeUI fadeUI;
    [SerializeField] MultipleChoiceRow [] multipleChoiceRow;
    [SerializeField] PlacePoint placePoint;
    [SerializeField] Transform doctorHand;
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

        GameObject doctor = GameObject.Find("DOCTOR");
        foreach (PlacePoint placePoint in doctor.GetComponentsInChildren<PlacePoint>())
        {
            this.placePoint = placePoint;
            this.placePoint.OnPlace.AddListener(SheetOnPlace);
        }
        foreach (Animator animator in doctor.GetComponentsInChildren<Animator>())
        {
            doctorHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        }

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

    void SheetOnPlace(PlacePoint placePoint,Grabbable grabbable) 
    {
        GetComponent<PhotonView>().RPC("SheetOnPlaceRPC", RpcTarget.All);

    }

    [PunRPC]
    void SheetOnPlaceRPC()
    {
        Debug.Log("SheetOnPlaceRPC");
        Destroy(fadeUI.GetComponent<PhotonTransformView>());
        if (PhotonManager._viewerApp)
        {
            fadeUI.transform.parent = doctorHand;
            fadeUI.transform.localPosition = new Vector3(0, 0, 0);
            fadeUI.transform.localEulerAngles = new Vector3(0, 0, 0);
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
