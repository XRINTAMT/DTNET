using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GloveApplicatorPhoton : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> multiplayerHands = new List<SkinnedMeshRenderer>();
    [SerializeField] MultiplayerController multiplayerController;

    bool setHands;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonManager._viewerApp)
        {
            GloveApplicator gloveApplicator = FindObjectOfType<GloveApplicator>();
            gloveApplicator.apply += Apply;
        }
    }
    

    public void Apply()
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("ApplyRPC", RpcTarget.All);


    }
    [PunRPC]
    void ApplyRPC()
    {
        Debug.Log("GlovesEvent_RPC");

        if (PhotonManager._viewerApp)
        {
            GloveApplicator gloveApplicator = FindObjectOfType<GloveApplicator>();
            for (int i = 0; i < multiplayerHands.Count; i++)
            {
                multiplayerHands[i].material = gloveApplicator.GloveMaterial;
            }
            gloveApplicator.gameObject.SetActive(false);
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<MultiplayerController>(true) && !setHands)
        {
            multiplayerController = FindObjectOfType<MultiplayerController>(true);

            foreach (SkinnedMeshRenderer skinRend in multiplayerController.rightHandFollower.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                multiplayerHands.Add(skinRend);
            }
            foreach (SkinnedMeshRenderer skinRend in multiplayerController.leftHandFollower.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                multiplayerHands.Add(skinRend);
            }

            setHands = true;
        }

   
    }
}
