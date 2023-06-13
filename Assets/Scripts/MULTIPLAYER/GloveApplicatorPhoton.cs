using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GloveApplicatorPhoton : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> multiplayerHands = new List<SkinnedMeshRenderer>();

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
            GloveApplicator gloveApplicator = GetComponent<GloveApplicator>();
            gloveApplicator.apply += Apply;
        }

        if (PhotonManager._viewerApp)
        {
            MultiplayerController multiplayerController = FindObjectOfType<MultiplayerController>(true);

            if (multiplayerController)
            {
                foreach (SkinnedMeshRenderer skinRend in multiplayerController.rightHandFollower.gameObject.GetComponentInChildren<Transform>())
                {
                    multiplayerHands.Add(skinRend);
                }
                foreach (SkinnedMeshRenderer skinRend in multiplayerController.leftHandFollower.gameObject.GetComponentInChildren<Transform>())
                {
                    multiplayerHands.Add(skinRend);
                }
            }
      
        }
    }
    

    public void Apply()
    {
        GetComponent<PhotonView>().RPC("ApplyRPC", RpcTarget.Others);
    }
    [PunRPC]
    void ApplyRPC()
    {
        if (PhotonManager._viewerApp)
        {
            GloveApplicator gloveApplicator = GetComponent<GloveApplicator>();
            for (int i = 0; i < multiplayerHands.Count; i++)
            {
                multiplayerHands[i].material = gloveApplicator.GloveMaterial;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
   
    }
}
