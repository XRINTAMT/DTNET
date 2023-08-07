using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AnimatorPhoton : MonoBehaviour
{
    AnimationsController animationsController;

    [SerializeField]AnimationMovement animationMovement;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        animationsController = FindObjectOfType<AnimationsController>();
        animationMovement = FindObjectOfType<AnimationMovement>();
        if (!PhotonManager._viewerApp)
        {
           animationsController.animationSeatingDown += AnimationSeatingDown;
           animationsController.animationLaying += AnimationLaying;
           animationsController.animationCallDoctor += AnimationCallDoctor;
           animationsController.animationArrivedDoctor += AnimationArrivedDoctor;
           animationMovement.walk += AnimationWalkDoctor;
           animationsController.animationInspectDoctor += AnimationInspectDoctor;
           animationsController.animationPutOffShirt += AnimationPutOffShirt;
           animationsController.animationWalkNurse += AnimationWalkNurse;
           animationsController.animationInjectNurse += AnimationInjectNurse;
           animationsController.animationStopNurse += AnimationStopNurse;
        }
    }
    public void AnimationSeatingDown() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationSeatingDownRPC", RpcTarget.AllBuffered);
    }
    public void AnimationLaying() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationLayingRPC", RpcTarget.AllBuffered);
    }
    public void AnimationCallDoctor() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationCallDoctorRPC", RpcTarget.AllBuffered);
    }
    public void AnimationArrivedDoctor()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationArrivedDoctorRPC", RpcTarget.AllBuffered);
    }
    public void AnimationWalkDoctor()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationWalkDoctorRPC", RpcTarget.AllBuffered);
    }
    public void AnimationInspectDoctor() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationInspectDoctorRPC", RpcTarget.AllBuffered);
    }
    public void AnimationPutOffShirt() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationPutOffShirtRPC", RpcTarget.AllBuffered);
    }
    public void AnimationWalkNurse() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationWalkNurseRPC", RpcTarget.AllBuffered);
    }
    public void AnimationInjectNurse() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationInjectNurseRPC", RpcTarget.AllBuffered);
    }
    public void AnimationStopNurse() 
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("AnimationStopNurseRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AnimationSeatingDownRPC()
    {
        Debug.Log("Animation_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationSeatDownPatient2();
        }
    }

    [PunRPC]
    public void AnimationLayingRPC()
    {
        Debug.Log("Animation_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationLayingPatient2();
        }
    }

    [PunRPC]
    public void AnimationCallDoctorRPC()
    {
        Debug.Log("CallDoctor_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.CallMrAdams();
        }
    }
    [PunRPC]
    public void AnimationArrivedDoctorRPC()
    {
        Debug.Log("ArrivedDoctor_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationArriveDoctor();
        }
    }
    [PunRPC]
    public void AnimationWalkDoctorRPC()
    {
        Debug.Log("WalkDoctor_RPC");
        if (PhotonManager._viewerApp)
        {
            animationMovement.StartMove(true);
        }
    }

    [PunRPC]
    public void AnimationInspectDoctorRPC()
    {
        Debug.Log("DoctorInspect_RPC");
        if (PhotonManager._viewerApp)
        {
            //animationsController.AnimationDoctorInspect();
        }
    }

    [PunRPC]
    public void AnimationPutOffShirtRPC()
    {
        Debug.Log("Animation_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.PutOffShirt();
        }
    }

    [PunRPC]
    public void AnimationWalkNurseRPC()
    {
        Debug.Log("WalkNurse_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationWalkNurse();
        }
    }

    [PunRPC]
    public void AnimationInjectNurseRPC()
    {
        Debug.Log("InjectNurse_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationNurseTakeInject();
        }
    }

    [PunRPC]
    public void AnimationStopNurseRPC()
    {
        Debug.Log("StopNurse_RPC");
        if (PhotonManager._viewerApp)
        {
            animationsController.AnimationStopNurse();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }



}
