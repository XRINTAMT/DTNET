using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TimeSkipPhoton : MonoBehaviour
{
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


            FadeMessageManager fadeMessageManager = FindObjectOfType<FadeMessageManager>();
            fadeMessageManager.fadeStart += FadeWithMessage;
        }
    }

    void FadeWithMessage(string text)
    {
        if (!PhotonManager._viewerApp)
            GetComponent<PhotonView>().RPC("FadeWithMessageRPC", RpcTarget.AllBuffered, text);
    }
    [PunRPC]
    void FadeWithMessageRPC(string text)
    {
        Debug.Log("Fade_RPC");
        if (PhotonManager._viewerApp)
        {
            FadeMessageManager fadeMessageManager = FindObjectOfType<FadeMessageManager>();
            fadeMessageManager.FadeWithText(text);
        }
    }
}
